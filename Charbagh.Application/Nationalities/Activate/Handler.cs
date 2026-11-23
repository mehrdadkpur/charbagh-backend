
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.Activate
{
    public sealed class ActivateNationalityHandler: IRequestHandler<ActivateNationalityCommand , NationalityDto>
    {
        private readonly INationalityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateNationalityHandler(INationalityRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NationalityDto> Handle (ActivateNationalityCommand request , CancellationToken cancellationToken)
        {
            var nationality = await _repository.GetByIdAsync(request.id, cancellationToken);
            if (nationality == null)
            {
                throw new NationalityNotFoundException(request.id);
            }
            nationality.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return nationality.ToDto();
        }
        
    }
}
