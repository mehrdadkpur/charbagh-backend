using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.GetAll
{
    public sealed record GetAllTeachersQuery:IRequest<IReadOnlyList<TeacherDto>>;
}
