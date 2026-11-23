using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Domain.Entities.People;

namespace Charbagh.Domain.Entities.Education
{
    public class StudentInstrument : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid TeacherUserId { get; set; }

        public Guid InstrumentId { get; set; }

        public DateOnly? StartTeachingDate { get; set; }

        public TeachingLevel TeachingLevel { get; set; }

        public decimal? DefaultSessionFee { get; set; }

        public bool IsActive { get; set; } = true;

        public Teacher Teacher { get; set; } = null!;

        public Instrument Instrument { get; set; } = null!;

        public Guid? DefaultBookId { get; set; }

        public Book? DefaultBook { get; set; }
    }


}
