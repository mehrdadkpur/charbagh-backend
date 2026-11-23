using Charbagh.Application.Abstractions.Security;
using Charbagh.Domain.Entities.Branches;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Charbagh.Infrastructure.Setup;

public sealed class InitialAdminBootstrapper
{
    private const string SuperAdminRoleName = "SuperAdmin";

    private readonly CharbaghDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;

    public InitialAdminBootstrapper(
        CharbaghDbContext context,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task BootstrapAsync( string initialPassword, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(initialPassword))
            throw new ArgumentException(
                "Initial password is required.",
                nameof(initialPassword));

        if (await _context.Set<User>()
                .AnyAsync(cancellationToken))
        {
            throw new InvalidOperationException(
                "Bootstrap is not allowed because users already exist.");
        }

        var firstName = Required("BootstrapAdmin:FirstName");
        var lastName = Required("BootstrapAdmin:LastName");
        var mobile = Required("BootstrapAdmin:Mobile");
        var nationalIdentificationNumber = Required("BootstrapAdmin:NationalIdentificationNumber");

        if (!Enum.TryParse<Gender>( Required("BootstrapAdmin:Gender"), ignoreCase: true, out var gender) || !Enum.IsDefined(gender))
            {
                throw new InvalidOperationException(
                    "BootstrapAdmin:Gender is invalid.");
            }
        var nationalityCode = Required("BootstrapAdmin:NationalityCode").ToUpperInvariant();

        var nationality = await _context.Set<Nationality>()
            .SingleOrDefaultAsync(
                x => x.Code == nationalityCode,
                cancellationToken);

        if (nationality is null)
        {
            throw new InvalidOperationException(
                $"Nationality with code '{nationalityCode}' was not found. " +
                "Run NationalitySeeder before bootstrapping the administrator.");
        }

        if (nationality.Status != NationalityStatus.Active)
        {
            throw new InvalidOperationException(
                $"Nationality '{nationalityCode}' is not active.");
        }

        var nationalityId = nationality.Id;

        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                cancellationToken);

        // Find an existing active branch or create the initial one.
        var branch = await _context.Set<Branch>()
            .Where(x => x.IsActive)
            .OrderBy(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (branch is null)
        {
            branch = Branch.Create(
                name: Required("BootstrapBranch:Name"),
                code: _configuration["BootstrapBranch:Code"],
                phoneNumber: _configuration["BootstrapBranch:PhoneNumber"],
                address: _configuration["BootstrapBranch:Address"]);

            _context.Set<Branch>().Add(branch);
        }

        // The permission catalog must already be seeded.
        var permissions = await _context.Set<Permission>()
            .ToListAsync(cancellationToken);

        if (permissions.Count == 0)
        {
            throw new InvalidOperationException(
                "No permissions exist. Seed the permission catalog first.");
        }

        var role = await _context.Set<Role>()
            .SingleOrDefaultAsync(
                x => x.Name == SuperAdminRoleName,
                cancellationToken);

        if (role is null)
        {
            role = Role.Create(
                name: SuperAdminRoleName,
                displayName: "مدیر کل سیستم",
                description: "System administrator",
                isSystem: true);

            _context.Set<Role>().Add(role);
        }
        else if (!role.IsActive || !role.IsSystem)
        {
            throw new InvalidOperationException(
                "The existing SuperAdmin role is inactive or not a system role.");
        }

        var user = User.Create(
            firstName: firstName,
            lastName: lastName,
            mobile: mobile,
            email: null,
            nationalityId: nationalityId,
            nationalIdentificationNumber:nationalIdentificationNumber,
            gender: gender,
            address: null,
            birthDate: null,
            bio: null,
            profileImageUrl: null);

        var (hash, salt) =
            _passwordHasher.HashPassword(initialPassword);

        user.SetPassword(hash, salt);

        _context.Set<User>().Add(user);

        // Connect the initial administrator to the branch.
        var userBranch = UserBranch.Create(
            userId: user.Id,
            branchId: branch.Id);

        userBranch.SetAsDefault();

        _context.Set<UserBranch>().Add(userBranch);

        // Grant the SuperAdmin role in that branch.
        var userBranchRole = UserBranchRole.Create(
            userBranchId: userBranch.Id,
            roleId: role.Id);

        _context.Set<UserBranchRole>().Add(userBranchRole);

        // Assign all existing system permissions to SuperAdmin.
        var existingPermissionIds = await _context
            .Set<RolePermission>()
            .Where(x => x.RoleId == role.Id)
            .Select(x => x.PermissionId)
            .ToListAsync(cancellationToken);

        var existingIds = existingPermissionIds.ToHashSet();

        foreach (var permission in permissions)
        {
            if (existingIds.Contains(permission.Id))
                continue;

            _context.Set<RolePermission>().Add(
                RolePermission.Create(role.Id, permission.Id));
        }

        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private string Required(string key)
    {
        var value = _configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(
                $"Required bootstrap configuration '{key}' is missing.");
        }

        return value.Trim();
    }
}