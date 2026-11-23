using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Domain.Entities.Settings
{
    public sealed class EducationService : BaseEntity
    {
        public string ServiceName { get; private set; } = null!;

        public string? Description { get; private set; }

        public bool IsActive { get; private set; } = true;


        // Navigation Properties

        public ICollection<ServicePrice> ServicePrices { get; private set; } = new List<ServicePrice>();


        // Constructor

        private EducationService()
        {
        }


        private EducationService(
            string serviceName,
            string? description)
        {
            ServiceName = serviceName;
            Description = description;
            IsActive = true;
        }


        // Factory

        public static EducationService Create(
            string serviceName,
            string? description = null)
        {
            Validate(serviceName);

            var educationService = new EducationService(
                serviceName.Trim(),
                description?.Trim());

            educationService.MarkAsCreated();

            return educationService;
        }


        // Update

        public void Update(
            string serviceName,
            string? description)
        {
            Validate(serviceName);

            ServiceName = serviceName.Trim();
            Description = description?.Trim();

            MarkAsUpdated();
        }


        // Status

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


        // Validation

        private static void Validate(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException(
                    "Service name is required.",
                    nameof(serviceName));
            }

            if (serviceName.Trim().Length > 100)
            {
                throw new ArgumentException(
                    "Service name cannot be longer than 100 characters.",
                    nameof(serviceName));
            }
        }
    }
}