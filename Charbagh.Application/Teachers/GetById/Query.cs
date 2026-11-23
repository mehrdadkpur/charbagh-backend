using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.GetById
{
    public sealed record GetTeacherByIdQuery(Guid Id) : IRequest<TeacherDto>;
}
