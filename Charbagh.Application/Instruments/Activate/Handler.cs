using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.Activate;

public sealed record ActivateInstrumentHandler : IRequestHandler<ActivateInstrumentCommand , InstrumentDto>
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateInstrumentHandler (IInstrumentRepository instrumentRepository, IUnitOfWork unitOfWork)
    {
        _instrumentRepository = instrumentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InstrumentDto> Handle(ActivateInstrumentCommand request , CancellationToken cancellationToken)
    {
        var instrument = await _instrumentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (instrument == null)
        {
            throw new InstrumentCategoryNotFoundException(request.Id);
        }
        if (!instrument.IsActive)
        {
            instrument.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
            return instrument.ToDto();
    }
}

