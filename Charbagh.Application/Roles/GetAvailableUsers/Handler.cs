using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Security;
using MediatR;

public sealed class GetAvailableUsersForRoleQueryHandler: IRequestHandler< GetAvailableUsersForRoleQuery, IReadOnlyList<AvailableRoleUserDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleManagementRepository _managementRepository;
    private readonly ICurrentBranch _currentBranch;

    public GetAvailableUsersForRoleQueryHandler(
        IRoleRepository roleRepository,
        IRoleManagementRepository managementRepository,
        ICurrentBranch currentBranch)
    {
        _roleRepository = roleRepository;
        _managementRepository = managementRepository;
        _currentBranch = currentBranch;
    }

    public async Task<IReadOnlyList<AvailableRoleUserDto>> Handle(
        GetAvailableUsersForRoleQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentBranch.HasBranch)
            throw new InvalidOperationException(
                "Active branch is not specified.");

        var branchId = _currentBranch.BranchId;

        Console.WriteLine($"RoleId: {request.RoleId}");
        Console.WriteLine($"CurrentBranchId: {branchId}");

        var role = await _roleRepository.GetByIdAsync(
            request.RoleId,
            cancellationToken);

        if (role is null)
            throw new KeyNotFoundException("Role not found.");

        var users = await _managementRepository.GetAvailableUsersAsync(
            request.RoleId,
            _currentBranch.BranchId,
            cancellationToken);

        Console.WriteLine($"Available users count: {users.Count}");

        return users
            .Select(x => new AvailableRoleUserDto(
                x.Id,
                x.UserId,
                x.User.FirstName,
                x.User.LastName,
                x.User.NationalIdentificationNumber,
                x.User.Mobile,
                x.User.Email,
                x.User.Status))
            .ToList();
    }
}