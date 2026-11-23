
namespace Charbagh.Domain.Entities.Identity
{
    public class Nationality : BaseEntity
    {
        public string EnglishCountryName { get; private set; } = null!;

        public string PersianCountryName { get; private set; } = null!;

        public string Code { get; private set; } = null!;

        public NationalityStatus Status { get; private set; } = NationalityStatus.Active;

        public ICollection<User> Users { get; private set; }= new List<User>();
        private Nationality()
        {
        }
        private Nationality(string englishCountryName, string persianCountryName , string code)
        {
            EnglishCountryName = englishCountryName.Trim();
            PersianCountryName = persianCountryName.Trim();
            Code = code.Trim().ToUpperInvariant();
            Status = NationalityStatus.Active;
        }

        public static Nationality Create(string englishCountryName, string persianCountryName, string code)
        {
            if (string.IsNullOrWhiteSpace(englishCountryName))
                throw new ArgumentException( "Nationality name is required.", nameof(englishCountryName));

            if (string.IsNullOrWhiteSpace(persianCountryName))
                throw new ArgumentException("Nationality name is required.", nameof(persianCountryName));

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException( "Nationality code is required.", nameof(code));

            var nationality = new Nationality(englishCountryName,persianCountryName, code);

            nationality.MarkAsCreated();

            return nationality;
        }

        public void Update(string englishCountryName, string persianCountryName , string code)
        {
            if (string.IsNullOrWhiteSpace(englishCountryName))
                throw new ArgumentException( "Nationality English Name is required.", nameof(englishCountryName));

            if (string.IsNullOrWhiteSpace(persianCountryName))
                throw new ArgumentException("Nationality Persian Name is required.", nameof(persianCountryName));

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException( "Nationality code is required.", nameof(code));

            EnglishCountryName = englishCountryName.Trim();
            PersianCountryName = persianCountryName.Trim();
            Code = code.Trim().ToUpperInvariant();

            MarkAsUpdated();
        }

        public void Activate()
        {
            if (Status == NationalityStatus.Pending)
            {
                throw new InvalidOperationException("Pending Nationality cannot be activated directly.");
            }

            if (Status == NationalityStatus.Active)
            {
                return;
            }

            Status = NationalityStatus.Active;
            MarkAsUpdated();
        }

        public void Deactivate()
        {
            if(Status == NationalityStatus.Pending)
            {
                throw new InvalidOperationException("Pending Nationality cannot be Deactivated.") ;
            }
            if (Status == NationalityStatus.Deactive)
                return;

            Status = NationalityStatus.Deactive;
            MarkAsUpdated();
        }
    }
}
