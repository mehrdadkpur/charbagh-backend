
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.GetById
{
    public sealed record GetLessonContentByIdQuery(Guid Id): IRequest<LessonContentDto>;
}
