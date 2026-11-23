
using Charbagh.Application.LessonContents.Common;
using MediatR;

namespace Charbagh.Application.LessonContents.SetReview
{
    public sealed record SetReviewLessonContentTypeCommand(Guid Id) : IRequest<LessonContentDto>;

}
