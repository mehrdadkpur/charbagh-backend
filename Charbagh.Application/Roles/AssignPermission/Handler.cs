using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Roles.AssignPermission;

public sealed class AddPermissionToRoleHandler
    : IRequestHandler<AddPermissionToRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPermissionToRoleHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        AddPermissionToRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(
            request.RoleId,
            cancellationToken);

        if (role is null)
            throw new KeyNotFoundException("Role not found.");

        if (!role.IsActive)
            throw new InvalidOperationException(
                "Cannot modify permissions of an inactive role.");

        var permission = await _permissionRepository.GetByIdAsync(
            request.PermissionId,
            cancellationToken);

        if (permission is null)
            throw new KeyNotFoundException("Permission not found.");

        var exists = await _rolePermissionRepository.ExistsAsync(
            request.RoleId,
            request.PermissionId,
            cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                "This permission is already assigned to the role.");

        await _rolePermissionRepository.AddAsync(
            RolePermission.Create(
                request.RoleId,
                request.PermissionId),
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}