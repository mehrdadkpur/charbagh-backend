using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.GetById
{
    public sealed record GetStudentParentByIdQuery(Guid Id): IRequest<StudentParentDto>;
}
