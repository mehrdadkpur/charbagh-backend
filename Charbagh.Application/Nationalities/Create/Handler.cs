
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Nationalities.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Nationalities.Create
{
    public sealed class CreateNationalityHandler : IRequestHandler<CreateNationalityCommand , NationalityDto>
    {
        private readonly INationalityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNationalityHandler(INationalityRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NationalityDto> Handle(CreateNationalityCommand request , CancellationToken cancellationToken )
        {
            var existsNationality = await _repository.ExistsByCodeAsync(request.Code, cancellationToken);

            if (existsNationality)
            {
                throw new DuplicateNationalityException(request.Code);
            }
            var nationality = Nationality.Create(request.EnglishCountryName, request.PersianCountryName, request.Code);
            await _repository.AddAsync(nationality , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return nationality.ToDto();
        }
    }
}
