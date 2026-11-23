using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.RolePermissions.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.RolePermissions.AssignToRole;

public sealed class AssignPermissionToRoleHandler
    : IRequestHandler<
        AssignPermissionToRoleCommand,
        RolePermissionDto>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignPermissionToRoleHandler(
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

    public async Task<RolePermissionDto> Handle( AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync( request.RoleId, cancellationToken);

        if (role is null)
        {
            throw new KeyNotFoundException(
                $"Role '{request.RoleId}' was not found.");
        }

        var permission = await _permissionRepository.GetByIdAsync( request.PermissionId, cancellationToken);

        if (permission is null)
        {
            throw new KeyNotFoundException( $"Permission '{request.PermissionId}' was not found.");
        }

        var exists = await _rolePermissionRepository.ExistsAsync( request.RoleId, request.PermissionId, cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException("This permission is already assigned to the role.");
        }

        var rolePermission = RolePermission.Create( request.RoleId, request.PermissionId);

        await _rolePermissionRepository.AddAsync( rolePermission, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RolePermissionDto( rolePermission.RoleId, rolePermission.PermissionId);
    }
}