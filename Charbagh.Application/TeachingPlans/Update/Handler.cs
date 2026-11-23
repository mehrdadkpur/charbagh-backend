
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeachingPlans.Common;
using MediatR;

namespace Charbagh.Application.TeachingPlans.Update
{
    public sealed class UpdateTeachingPlanHandler : IRequestHandler<UpdateTeachingPlanCommand, TeachingPlanDto>
    {
        private readonly ITeachingPlanRepository _teachingPlanRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeachingPlanHandler(
           ITeachingPlanRepository teachingPlanRepository,
           IBookItemRepository bookItemRepository,
           IUnitOfWork unitOfWork)
        {
            _teachingPlanRepository = teachingPlanRepository;
            _bookItemRepository = bookItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeachingPlanDto> Handle(UpdateTeachingPlanCommand request, CancellationToken cancellationToken)
        {

            var teachingPlan = await _teachingPlanRepository.GetByIdAsync(request.Id, cancellationToken);

            if (teachingPlan == null)
            {
                throw new TeachingPlanNotFoundException(request.Id);
            }

            if (teachingPlan.Status != TeachingPlanStatus.Active)
                throw new InvalidOperationException("Only active Teaching Plans can be updated.");

            var startBookItem = await _bookItemRepository.GetByIdAsync(request.StartBookItemId, cancellationToken);
            if (startBookItem is null)
                throw new BookItemNotFoundException(request.StartBookItemId);

            // StartBookItem باید متعلق به همان Book باشد
            if (startBookItem.BookId != teachingPlan.BookId)
            {
                throw new InvalidOperationException("Start book item does not belong to the teaching plan book.");
            }
            // 4. بررسی TargetBookItem
            if (request.TargetBookItemId.HasValue)
            {
                var targetBookItem = await _bookItemRepository.GetByIdAsync(request.TargetBookItemId.Value, cancellationToken);
                if (targetBookItem is null)
                    throw new BookItemNotFoundException(request.TargetBookItemId.Value);
                if (targetBookItem.BookId != teachingPlan.BookId)
                {
                    throw new InvalidOperationException("Target book item does not belong to the teaching plan book.");
                }
            }

                // 5. بررسی DisplayOrder تکراری

                var existsDisplayOrder = await _teachingPlanRepository.ExistsByLessonAssignmentAndDisplayOrderAsync(teachingPlan.LessonAssignmentId, request.DisplayOrder, request.Id, cancellationToken);

                if (existsDisplayOrder) { throw new InvalidOperationException("Another Teaching Plan with the same display order already exists."); }

                teachingPlan.Update(
                request.StartBookItemId,
                request.TargetBookItemId,
                request.StartDate,
                request.EndDate,
                request.DisplayOrder,
                request.Objective,
                request.Notes);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return teachingPlan.ToDto();

            }
        }
    }

