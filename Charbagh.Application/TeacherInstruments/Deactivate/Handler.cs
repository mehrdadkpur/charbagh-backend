using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Deactivate;

public sealed class DeactivateTeacherInstrumentHandler
    : IRequestHandler<
        DeactivateTeacherInstrumentCommand,
        TeacherInstrumentDto>
{
    private readonly ITeacherInstrumentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateTeacherInstrumentHandler(
        ITeacherInstrumentRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeacherInstrumentDto> Handle(
        DeactivateTeacherInstrumentCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetForUpdateAsync(
            request.Id,
            cancellationToken);

        if (item is null)
            throw new TeacherInstrumentNotFoundException(
                request.Id);

        item.Deactivate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return item.ToDto();
    }
}