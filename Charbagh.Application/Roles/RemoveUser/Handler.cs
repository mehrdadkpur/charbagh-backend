using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Roles.RemoveUser;
using MediatR;

public sealed class RemoveUserFromRoleHandler
    : IRequestHandler<RemoveUserFromRoleCommand>
{
    private readonly IRoleManagementRepository _repository;
    private readonly ICurrentBranch _currentBranch;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserFromRoleHandler(
        IRoleManagementRepository repository,
        ICurrentBranch currentBranch,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _currentBranch = currentBranch;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        RemoveUserFromRoleCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentBranch.HasBranch)
            throw new InvalidOperationException(
                "Active branch is not specified.");

        var userBranch = await _repository.GetUserBranchAsync(
            request.UserBranchId,
            cancellationToken);

        if (userBranch is null)
            throw new KeyNotFoundException("User branch not found.");

        if (userBranch.BranchId != _currentBranch.BranchId)
            throw new InvalidOperationException(
                "User does not belong to the active branch.");

        var assignment = await _repository.GetUserBranchRoleAsync(
            request.RoleId,
            request.UserBranchId,
            cancellationToken);

        if (assignment is null || !assignment.IsActive)
            throw new KeyNotFoundException("Active role assignment not found.");

        assignment.Deactivate();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}