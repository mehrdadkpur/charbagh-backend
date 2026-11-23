using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Activate;

public sealed class ActivateTeacherInstrumentHandler : IRequestHandler< ActivateTeacherInstrumentCommand, TeacherInstrumentDto>
{
    private readonly ITeacherInstrumentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateTeacherInstrumentHandler(
        ITeacherInstrumentRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeacherInstrumentDto> Handle(
        ActivateTeacherInstrumentCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetForUpdateAsync(
            request.Id,
            cancellationToken);

        if (item is null)
            throw new TeacherInstrumentNotFoundException(
                request.Id);

        item.Activate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return item.ToDto();
    }
}