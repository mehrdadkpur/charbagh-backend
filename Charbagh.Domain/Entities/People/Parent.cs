using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.People
{
    public class Parent : PersonProfile
    {
        public ParentStatus Status { get; private set; }

        public ICollection<StudentParent> StudentParents { get; private set; } = new List<StudentParent>();

        private Parent()
        {
        }

        private Parent(Guid userId)
        {
            UserId = userId;
            Status = ParentStatus.Pending;
        }

        public static Parent Create(Guid userId)
        {
            var parent = new Parent(userId);

            parent.MarkAsCreated();

            return parent;
        }

        public void Activate()
        {
            if (Status == ParentStatus.Active)
                return;
            if(Status == ParentStatus.Blocked)
            {
                throw new Exception("Blocked Parent cannot be activated directly.");
            }
            Status = ParentStatus.Active;

            MarkAsUpdated();
        }

        public void Suspend()
        {
            if (Status == ParentStatus.Suspended)
                return;

            if (Status != ParentStatus.Active)
                throw new InvalidOperationException("Only an Active Parent can be suspended.");

            Status = ParentStatus.Suspended;
            MarkAsUpdated();
        }

        public void Block()
        {
            if (Status == ParentStatus.Blocked)
                return;

            Status = ParentStatus.Blocked;
            MarkAsUpdated();
        }

        public void Unblock()
        {
            if (Status != ParentStatus.Blocked)
                throw new InvalidOperationException("Only a blocked Parent can be unblocked.");

            Status = ParentStatus.Active;
            MarkAsUpdated();
        }
    }
}
