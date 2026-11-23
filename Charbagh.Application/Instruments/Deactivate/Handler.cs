using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.Deactivate;

public sealed record DeactivateInstrumentHandler : IRequestHandler<DeactivateInstrumentCommand ,InstrumentDto>
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateInstrumentHandler (IInstrumentRepository instrumentRepository, IUnitOfWork unitOfWork)
    {
        _instrumentRepository = instrumentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InstrumentDto> Handle (DeactivateInstrumentCommand request , CancellationToken cancellationToken)
    {
        var instrument = await _instrumentRepository.GetByIdAsync(request.Id , cancellationToken);
        if(instrument == null)
        {
            throw new InstrumentNotFoundException(request.Id);
        }
        if (instrument.IsActive)
        {
            instrument.Deactivate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    return instrument.ToDto();
    }
}

