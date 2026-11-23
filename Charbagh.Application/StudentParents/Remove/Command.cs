
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.Remove
{
    public sealed record RemoveStudentParentCommand(Guid Id): IRequest;
}
