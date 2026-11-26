using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Instruments.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Instruments.Update;

public sealed record UpdateInstrumentHandler: IRequestHandler<UpdateInstrumentCommand , InstrumentDto>
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInstrumentHandler(IInstrumentRepository instrumentRepository, IUnitOfWork unitOfWork)
    {
        _instrumentRepository = instrumentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task <InstrumentDto> Handle (UpdateInstrumentCommand request , CancellationToken cancellationToken)
    {
        var instrument = await _instrumentRepository.GetByIdAsync(request.Id, cancellationToken);

        if(instrument == null)
        {
            throw new InstrumentNotFoundException(request.Id);
        }

        var existing = await _instrumentRepository.GetByNameAsync(request.InstrumentCategoryId,request.Name, cancellationToken);

        if (existing is not null && existing.Id != request.Id)
        {
            throw new DuplicateInstrumentException(request.Name);
        }

        instrument.Update(request.InstrumentCategoryId, request.Name, request.EnglishName, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return instrument.ToDto();
    }
}

