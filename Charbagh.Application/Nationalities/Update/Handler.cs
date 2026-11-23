using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.Update
{
    public sealed class UpdateNationalityHandler: IRequestHandler<UpdateNationalityCommand , NationalityDto>
    {
        private readonly INationalityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNationalityHandler(INationalityRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NationalityDto> Handle(UpdateNationalityCommand request , CancellationToken cancellationToken)
        {
            var nationality = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if (nationality == null)
            {
                throw new NationalityNotFoundException(request.Id);
            }

            nationality.Update(request.EnglishCountryName, request.PersianCountryName, request.Code);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return nationality.ToDto();
        }
    }
}
