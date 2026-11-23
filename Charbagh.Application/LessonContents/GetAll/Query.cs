
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.GetAll
{
    public sealed record GetAllLessonContentsQuery(): IRequest<IReadOnlyList< LessonContentDto>>;
}
