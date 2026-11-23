using Charbagh.Domain.Entities.Settings;

namespace Charbagh.Domain.Entities.Finance
{
    public sealed class ServicePrice : BaseEntity
    {
        public Guid EducationServiceId { get; private set; }

        public decimal Price { get; private set; }

        public DateOnly EffectiveFrom { get; private set; }

        public DateOnly? EffectiveTo { get; private set; }

        public bool IsActive { get; private set; }

        // Navigation Property

        public EducationService EducationService { get; private set; } = null!;


        // Constructor

        private ServicePrice()
        {
        }


        private ServicePrice(
            Guid educationServiceId,
            decimal price,
            DateOnly effectiveFrom)
        {
            EducationServiceId = educationServiceId;
            Price = price;
            EffectiveFrom = effectiveFrom;
            IsActive = true;
        }


        // Factory

        public static ServicePrice Create(
            Guid educationServiceId,
            decimal price,
            DateOnly effectiveFrom)
        {
            if (educationServiceId == Guid.Empty)
            {
                throw new ArgumentException(
                    "Education service is required.",
                    nameof(educationServiceId));
            }

            if (price <= 0)
            {
                throw new ArgumentException(
                    "Price must be greater than zero.",
                    nameof(price));
            }

            var servicePrice = new ServicePrice(
                educationServiceId,
                price,
                effectiveFrom);

            servicePrice.MarkAsCreated();

            return servicePrice;
        }


        // Update Price

        public void UpdatePrice(decimal price)
        {
            if (price <= 0)
            {
                throw new ArgumentException(
                    "Price must be greater than zero.",
                    nameof(price));
            }

            Price = price;

            MarkAsUpdated();
        }


        // Set Effective To

        public void SetEffectiveTo(DateOnly effectiveTo)
        {
            if (effectiveTo < EffectiveFrom)
            {
                throw new ArgumentException(
                    "Effective to date cannot be earlier than effective from date.",
                    nameof(effectiveTo));
            }

            EffectiveTo = effectiveTo;
            IsActive = false;

            MarkAsUpdated();
        }


        // Activate

        public void Activate()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            EffectiveTo = null;

            MarkAsUpdated();
        }


        // Deactivate

        public void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;

            MarkAsUpdated();
        }
    }
}
