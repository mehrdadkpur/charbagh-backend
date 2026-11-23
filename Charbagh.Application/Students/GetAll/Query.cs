using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.GetAll
{
    public sealed record GetAllStudentsQuery: IRequest<IReadOnlyList<StudentDto>>;
}
