using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Instruments.Common;
using Charbagh.Application.Instruments.Create;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using MediatR;

public sealed class CreateInstrumentHandler : IRequestHandler<CreatebookCommand, InstrumentDto>
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IInstrumentCategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInstrumentHandler(IInstrumentRepository instrumentRepository, IInstrumentCategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _instrumentRepository = instrumentRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InstrumentDto> Handle( CreatebookCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync( request.InstrumentCategoryId, cancellationToken);

            if (category is null)
                throw new InstrumentCategoryNotFoundException(
                    request.InstrumentCategoryId);

            var exists = await _instrumentRepository.GetByNameAsync( request.InstrumentCategoryId, request.Name, cancellationToken);

            if (exists is not null)
                throw new DuplicateInstrumentException(request.Name);

            var instrument = Instrument.Create(
                request.InstrumentCategoryId,
                request.Name,
                request.EnglishName,
                request.Description);

            await _instrumentRepository.AddAsync( instrument, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return instrument.ToDto();
        }
    }
