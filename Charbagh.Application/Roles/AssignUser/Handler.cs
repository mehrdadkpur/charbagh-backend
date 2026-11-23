using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Roles.AssignUser;
using Charbagh.Domain.Entities.Identity;
using MediatR;

public sealed class AssignUserToRoleCommandHandler
    : IRequestHandler<AssignUserToRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleManagementRepository _managementRepository;
    private readonly ICurrentBranch _currentBranch;
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserToRoleCommandHandler(
        IRoleRepository roleRepository,
        IRoleManagementRepository managementRepository,
        ICurrentBranch currentBranch,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _managementRepository = managementRepository;
        _currentBranch = currentBranch;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        AssignUserToRoleCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentBranch.HasBranch)
            throw new InvalidOperationException(
                "Active branch is not specified.");

        var role = await _roleRepository.GetByIdAsync(
            request.RoleId,
            cancellationToken);

        if (role is null)
            throw new KeyNotFoundException("Role not found.");

        if (!role.IsActive)
            throw new InvalidOperationException(
                "Cannot assign users to an inactive role.");

        var userBranch = await _managementRepository.GetUserBranchAsync(
            request.UserBranchId,
            cancellationToken);

        if (userBranch is null)
            throw new KeyNotFoundException("User branch not found.");

        if (userBranch.BranchId != _currentBranch.BranchId)
            throw new InvalidOperationException(
                "User does not belong to the active branch.");

        if (!userBranch.IsActive)
            throw new InvalidOperationException(
                "Cannot assign a role to an inactive user branch.");

        var existing = await _managementRepository.GetUserBranchRoleAsync(
            request.RoleId,
            request.UserBranchId,
            cancellationToken);

        if (existing is not null)
        {
            if (existing.IsActive)
                throw new InvalidOperationException(
                    "This user already has this role in this branch.");

            existing.Activate();
        }
        else
        {
            var assignment = UserBranchRole.Create(
                request.UserBranchId,
                request.RoleId);

            await _managementRepository.AddUserBranchRoleAsync(
                assignment,
                cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}