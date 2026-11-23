using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Users.Deactivate
{
    public sealed class DeactivateNationalityHandler : IRequestHandler<DeactivateNationalityCommand, NationalityDto>
    {
        private readonly INationalityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateNationalityHandler(INationalityRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NationalityDto> Handle(DeactivateNationalityCommand request, CancellationToken cancellationToken)
        {
            var nationality = await _repository.GetByIdAsync(request.id, cancellationToken);
            if (nationality == null)
            {
                throw new NationalityNotFoundException(request.id);
            }

            if(nationality.Status == NationalityStatus.Active)
            {
                nationality.Deactivate();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return nationality.ToDto();
        }

    }

}
