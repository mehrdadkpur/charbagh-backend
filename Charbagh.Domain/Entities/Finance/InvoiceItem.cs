
namespace Charbagh.Domain.Entities.Finance
{
    public sealed class InvoiceItem : BaseEntity
    {
        public Guid InvoiceId { get; private set; }

        public Guid? ServicePriceId { get; private set; }

        public InvoiceItemType Type { get; private set; }

        public string Description { get; private set; } = null!;

        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }

        public decimal TotalAmount => UnitPrice * Quantity;


        // Navigation Properties

        public Invoice Invoice { get; private set; } = null!;

        public ServicePrice? ServicePrice { get; private set; }


        // Constructor

        private InvoiceItem()
        {
        }


        private InvoiceItem(
            Guid invoiceId,
            Guid? servicePriceId,
            InvoiceItemType type,
            string description,
            decimal unitPrice,
            int quantity)
        {
            InvoiceId = invoiceId;
            ServicePriceId = servicePriceId;
            Type = type;
            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }


        // Factory

        public static InvoiceItem Create(
            Guid invoiceId,
            Guid? servicePriceId,
            InvoiceItemType type,
            string description,
            decimal unitPrice,
            int quantity)
        {
            if (invoiceId == Guid.Empty)
            {
                throw new ArgumentException( "Invoice is required.", nameof(invoiceId));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException( "Description is required.", nameof(description));
            }

            if (unitPrice < 0)
            {
                throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

            if (type != InvoiceItemType.Other && servicePriceId == null) 
            { 
                throw new ArgumentException("Service price is required for this invoice item type.", nameof(servicePriceId));
            }

            if (type == InvoiceItemType.Other && servicePriceId != null) 
            { 
                throw new ArgumentException("Other invoice items cannot have a service price.", nameof(servicePriceId)); 
            }

            var invoiceItem = new InvoiceItem(
                invoiceId,
                servicePriceId,
                type,
                description.Trim(),
                unitPrice,
                quantity);

            invoiceItem.MarkAsCreated();

            return invoiceItem;
        }


        // Update

        public void Update(
            Guid? servicePriceId,
            InvoiceItemType type,
            string description,
            decimal unitPrice,
            int quantity)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException( "Description is required.", nameof(description));
            }

            if (unitPrice < 0)
            {
                throw new ArgumentException( "Unit price cannot be negative.", nameof(unitPrice));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException( "Quantity must be greater than zero.", nameof(quantity));
            }

            if (type != InvoiceItemType.Other && servicePriceId == null) 
            { 
                throw new ArgumentException("Service price is required for this invoice item type.", nameof(servicePriceId));
            }

            if (type == InvoiceItemType.Other && servicePriceId != null) 
            { 
                throw new ArgumentException("Other invoice items cannot have a service price.", nameof(servicePriceId));
            }

            ServicePriceId = servicePriceId;
            Type = type;
            Description = description.Trim();
            UnitPrice = unitPrice;
            Quantity = quantity;

            MarkAsUpdated();
        }  
    
    }
}