using Charbagh.Domain.Entities.Finance;
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Domain.Entities.Branches
{
    public sealed class Branch : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public string? Code { get; private set; }

        public string? PhoneNumber { get; private set; }

        public string? Address { get; private set; }

        public bool IsActive { get; private set; }

        // Navigation Properties

        public ICollection<UserBranch> UserBranches { get; private set; } = new List<UserBranch>();

        public ICollection<Invoice> Invoices { get; private set; } = new List<Invoice>();

        public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

        public ICollection<Classroom> Classrooms { get; private set; } = new List<Classroom>();

        private Branch()
        {
        }

        public static Branch Create(
            string name,
            string? code,
            string? phoneNumber,
            string? address)
                {
            var branch = new Branch
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Code = code?.Trim(),
                PhoneNumber = phoneNumber?.Trim(),
                Address = address?.Trim(),
                IsActive = true
            };

            branch.MarkAsCreated();

            return branch;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Update(string name,string? code,string? phoneNumber,string? address)
        {
            Name = name;
            Code = code;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }

}