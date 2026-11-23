using Charbagh.Domain.Entities.Education.EducationCatalog;

    namespace Charbagh.Domain.Entities.People;

    public sealed class TeacherInstrument : BaseEntity
    {
        public Guid TeacherId { get; private set; }

        public Guid InstrumentId { get; private set; }

        public DateOnly? StartTeachingDate { get; private set; }

        public TeachingLevel TeachingLevel { get; private set; }

        public decimal? DefaultSessionFee { get; private set; }

        public bool IsActive { get; private set; }

        public Guid? DefaultBookId { get; private set; }


        // Navigation Properties

        public Teacher Teacher { get; private set; } = null!;

        public Instrument Instrument { get; private set; } = null!;

        public Book? DefaultBook { get; private set; }


        // EF Core
        private TeacherInstrument()
        {
        }


        private TeacherInstrument(
            Guid teacherId,
            Guid instrumentId,
            DateOnly? startTeachingDate,
            TeachingLevel teachingLevel,
            decimal? defaultSessionFee,
            Guid? defaultBookId)
        {
            TeacherId = teacherId;
            InstrumentId = instrumentId;
            StartTeachingDate = startTeachingDate;
            TeachingLevel = teachingLevel;
            DefaultSessionFee = defaultSessionFee;
            DefaultBookId = defaultBookId;
            IsActive = true;
        }


        public static TeacherInstrument Create(
            Guid teacherId,
            Guid instrumentId,
            TeachingLevel teachingLevel,
            DateOnly? startTeachingDate = null,
            decimal? defaultSessionFee = null,
            Guid? defaultBookId = null)
        {
            ValidateIds(teacherId, instrumentId);

            ValidateSessionFee(defaultSessionFee);

            var teacherInstrument = new TeacherInstrument(
                teacherId,
                instrumentId,
                startTeachingDate,
                teachingLevel,
                defaultSessionFee,
                defaultBookId);

            teacherInstrument.MarkAsCreated();

            return teacherInstrument;
        }


        public void Update(
            TeachingLevel teachingLevel,
            DateOnly? startTeachingDate,
            decimal? defaultSessionFee,
            Guid? defaultBookId)
        {
            ValidateSessionFee(defaultSessionFee);

            TeachingLevel = teachingLevel;
            StartTeachingDate = startTeachingDate;
            DefaultSessionFee = defaultSessionFee;
            DefaultBookId = defaultBookId;

            MarkAsUpdated();
        }


        public void ChangeDefaultBook(Guid? bookId)
        {
            DefaultBookId = bookId;

            MarkAsUpdated();
        }


        public void ChangeDefaultSessionFee(decimal? sessionFee)
        {
            ValidateSessionFee(sessionFee);

            DefaultSessionFee = sessionFee;

            MarkAsUpdated();
        }


        public void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;

            MarkAsUpdated();
        }


        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;

            MarkAsUpdated();
        }


        private static void ValidateIds(
            Guid teacherId,
            Guid instrumentId)
        {
            if (teacherId == Guid.Empty)
                throw new ArgumentException(
                    "Teacher id is required.",
                    nameof(teacherId));

            if (instrumentId == Guid.Empty)
                throw new ArgumentException(
                    "Instrument id is required.",
                    nameof(instrumentId));
        }


        private static void ValidateSessionFee(
            decimal? sessionFee)
        {
            if (sessionFee.HasValue && sessionFee.Value < 0)
            {
                throw new ArgumentException(
                    "Session fee cannot be negative.",
                    nameof(sessionFee));
            }
        }
    }
