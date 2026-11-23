namespace Charbagh.Domain.Entities.Finance
{
    public sealed class PaymentMethod : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public int DisplayOrder { get; private set; }

        public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

        private PaymentMethod()
        {
        }

        private PaymentMethod(
            string name,
            string? description,
            int displayOrder)
        {
            Name = name.Trim();
            Description = description?.Trim();
            DisplayOrder = displayOrder;
            IsActive = true;
        }

        public static PaymentMethod Create(
            string name,
            string? description = null,
            int displayOrder = 0)
        {
            ValidateName(name);
            ValidateDisplayOrder(displayOrder);

            var paymentMethod = new PaymentMethod(
                name,
                description,
                displayOrder);

            paymentMethod.MarkAsCreated();

            return paymentMethod;
        }

        public void Update(
            string name,
            string? description,
            int displayOrder)
        {
            ValidateName(name);
            ValidateDisplayOrder(displayOrder);

            Name = name.Trim();
            Description = description?.Trim();
            DisplayOrder = displayOrder;

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

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    "Payment method name is required.",
                    nameof(name));
        }

        private static void ValidateDisplayOrder(int displayOrder)
        {
            if (displayOrder < 0)
                throw new ArgumentException(
                    "Display order cannot be negative.",
                    nameof(displayOrder));
        }
    }
}
