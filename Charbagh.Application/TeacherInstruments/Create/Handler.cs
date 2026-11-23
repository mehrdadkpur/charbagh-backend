using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.People;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.TeacherInstruments.Common;
using Charbagh.Domain.Entities.People;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Create;

public sealed class CreateTeacherInstrumentHandler: IRequestHandler<CreateTeacherInstrumentCommand,TeacherInstrumentDto>
{
    private readonly ITeacherInstrumentRepository _repository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeacherInstrumentHandler(
        ITeacherInstrumentRepository repository,
        ITeacherRepository teacherRepository,
        IInstrumentRepository instrumentRepository,
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _teacherRepository = teacherRepository;
        _instrumentRepository = instrumentRepository;
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeacherInstrumentDto> Handle(
        CreateTeacherInstrumentCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetByIdAsync(
            request.TeacherId,
            cancellationToken);

        if (teacher is null)
            throw new TeacherNotFoundException();

        var instrument = await _instrumentRepository.GetByIdAsync(
            request.InstrumentId,
            cancellationToken);

        if (instrument is null)
            throw new InstrumentNotFoundException(request.InstrumentId);

        if (await _repository.ExistsAsync(
                request.TeacherId,
                request.InstrumentId,
                cancellationToken))
        {
            throw new InvalidOperationException(
                "This teacher is already assigned to this instrument.");
        }

        if (request.DefaultBookId.HasValue)
        {
            var book = await _bookRepository.GetByIdAsync(
                request.DefaultBookId.Value,
                cancellationToken);

            if (book is null)
                throw new BookNotFoundException(
                    request.DefaultBookId.Value);

            if (book.InstrumentId != request.InstrumentId)
            {
                throw new InvalidOperationException(
                    "The default book must belong to the selected instrument.");
            }
        }

        var teacherInstrument = TeacherInstrument.Create(
            request.TeacherId,
            request.InstrumentId,
            request.TeachingLevel,
            request.StartTeachingDate,
            request.DefaultSessionFee,
            request.DefaultBookId);

        await _repository.AddAsync(
            teacherInstrument,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return teacherInstrument.ToDto();
    }
}