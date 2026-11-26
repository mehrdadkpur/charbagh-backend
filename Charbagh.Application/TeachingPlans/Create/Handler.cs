using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeachingPlans.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Create
{
    public sealed class CreateTeachingPlanHandler : IRequestHandler<CreateTeachingPlanCommand , TeachingPlanDto>
    {
        private readonly ITeachingPlanRepository _teachingPlanRepository;
        private readonly ILessonAssignmentRepository _lessonAssignmentRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeachingPlanHandler(
           ITeachingPlanRepository teachingPlanRepository,
           ILessonAssignmentRepository lessonAssignmentRepository,
           IBookRepository bookRepository,
           IBookItemRepository bookItemRepository,
           IUnitOfWork unitOfWork)
        {
            _teachingPlanRepository = teachingPlanRepository;
            _lessonAssignmentRepository = lessonAssignmentRepository;
            _bookRepository = bookRepository;
            _bookItemRepository = bookItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <TeachingPlanDto> Handle(CreateTeachingPlanCommand request , CancellationToken cancellationToken)
        {
            // 1. LessonAssignment

            var lessonAssignment = await _lessonAssignmentRepository.GetByIdAsync( request.LessonAssignmentId,cancellationToken);

            if (lessonAssignment is null)
            {
                throw new LessonAssignmentNotFoundException(request.LessonAssignmentId);
            }

            // 2. Book

            var book = await _bookRepository.GetByIdAsync( request.BookId, cancellationToken);

            if (book is null)
            {
                throw new BookNotFoundException(request.BookId);
            }

            // 3. Active TeachingPlan

            var existsActiveTeachingPlan =
                await _teachingPlanRepository
                    .ExistsActiveByLessonAssignmentIdAsync(
                        request.LessonAssignmentId,
                        null,
                        cancellationToken);

            if (existsActiveTeachingPlan)
            {
                throw new ActiveTeachingPlanAlreadyExistsException( request.LessonAssignmentId);
            }

            var teachingPlan = TeachingPlan.Create(
                request.LessonAssignmentId,
                request.BookId,
                request.StartBookItemId,
                request.TargetBookItemId,
                request.StartDate,
                request.DisplayOrder,
                request.Objective,
                request.Notes,
                request.EndDate);

            await _teachingPlanRepository.AddAsync(teachingPlan , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return teachingPlan.ToDto();

        }
    }
}
