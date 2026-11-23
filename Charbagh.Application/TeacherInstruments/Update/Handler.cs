using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Update;

public sealed class UpdateTeacherInstrumentHandler: IRequestHandler< UpdateTeacherInstrumentCommand,TeacherInstrumentDto>
{
    private readonly ITeacherInstrumentRepository _repository;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeacherInstrumentHandler(
        ITeacherInstrumentRepository repository,
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeacherInstrumentDto> Handle(
        UpdateTeacherInstrumentCommand request,
        CancellationToken cancellationToken)
    {
        var teacherInstrument =
            await _repository.GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (teacherInstrument is null)
            throw new TeacherInstrumentNotFoundException(
                request.Id);

        if (request.DefaultBookId.HasValue)
        {
            var book = await _bookRepository.GetByIdAsync(
                request.DefaultBookId.Value,
                cancellationToken);

            if (book is null)
                throw new BookNotFoundException(
                    request.DefaultBookId.Value);

            if (book.InstrumentId != teacherInstrument.InstrumentId)
            {
                throw new InvalidOperationException(
                    "The default book must belong to the teacher's instrument.");
            }
        }

        teacherInstrument.Update(
            request.TeachingLevel,
            request.StartTeachingDate,
            request.DefaultSessionFee,
            request.DefaultBookId);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return teacherInstrument.ToDto();
    }
}