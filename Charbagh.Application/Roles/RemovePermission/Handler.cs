using Charbagh.Application.Abstractions.Persistence;
using MediatR;

namespace Charbagh.Application.Roles.RemovePermission;

public sealed class RemovePermissionFromRoleHandler
    : IRequestHandler<RemovePermissionFromRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePermissionFromRoleHandler(
        IRoleRepository roleRepository,
        IRolePermissionRepository rolePermissionRepository,
        IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        RemovePermissionFromRoleCommand request,
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

        await _rolePermissionRepository.RemoveAsync(
            request.RoleId,
            request.PermissionId,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}