namespace Charbagh.Domain.Entities.CMS
{
    public sealed class Contact : BaseEntity
    {
        public string FullName { get; private set; } = null!;
        public string Mobile { get; private set; } = null!;
        public ContactStatus Status { get; private set; }

        private Contact()
        {
        }

        private Contact(
            string fullName,
            string mobile)
        {
            FullName = fullName;
            Mobile = mobile;
            Status = ContactStatus.New;
        }

        public static Contact Create(
            string fullName,
            string mobile
            )
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException(
                    "Full name is required.",
                    nameof(fullName));

            if (string.IsNullOrWhiteSpace(mobile))
                throw new ArgumentException(
                    "Mobile is required.",
                    nameof(mobile));

            var contact = new Contact(
                fullName.Trim(),
                mobile.Trim()
                );

            contact.MarkAsCreated();

            return contact;
        }

        public void StartProcessing()
        {
            if (Status == ContactStatus.Closed)
                throw new InvalidOperationException(
                    "Closed contacts cannot be processed.");

            if (Status == ContactStatus.InProgress)
                return;

            Status = ContactStatus.InProgress;
            MarkAsUpdated();
        }

        public void MarkAsReplied()
        {
            if (Status == ContactStatus.Closed)
                throw new InvalidOperationException(
                    "Closed contacts cannot be replied.");

            if (Status == ContactStatus.Replied)
                return;

            Status = ContactStatus.Replied;
            MarkAsUpdated();
        }

        public void Close()
        {
            if (Status == ContactStatus.Closed)
                return;

            Status = ContactStatus.Closed;
            MarkAsUpdated();
        }

        public void Reopen()
        {
            if (Status != ContactStatus.Closed)
                return;

            Status = ContactStatus.New;
            MarkAsUpdated();
        }

        public bool IsNew =>
            Status == ContactStatus.New;

        public bool IsInProgress =>
            Status == ContactStatus.InProgress;

        public bool IsReplied =>
            Status == ContactStatus.Replied;

        public bool IsClosed =>
            Status == ContactStatus.Closed;
    }
}