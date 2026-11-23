
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.GetById
{
    public sealed record GetStudentByIdQuery(Guid Id) : IRequest<StudentDto>;
}
