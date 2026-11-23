using Charbagh.Domain.Entities.CMS;
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Domain.Entities.CMS
{
    public sealed class Comment : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid BlogId { get; private set; }
        public string Text { get; private set; } = null!;
        public CommentStatus Status { get; private set; }
        public User User { get; private set; } = null!;
        public Blog Blog { get; private set; } = null!;

        private Comment()
        {
        }

        private Comment(
            Guid userId,
            Guid blogId,
            string text)
        {
            UserId = userId;
            BlogId = blogId;
            Text = text;
            Status = CommentStatus.Pending;
        }

        public static Comment Create(
            Guid userId,
            Guid blogId,
            string text)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(
                    "User is required.",
                    nameof(userId));

            if (blogId == Guid.Empty)
                throw new ArgumentException(
                    "Blog is required.",
                    nameof(blogId));

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(
                    "Comment text is required.",
                    nameof(text));

            var comment = new Comment(
                userId,
                blogId,
                text.Trim());

            comment.MarkAsCreated();

            return comment;
        }

        public void Update(string text)
        {
            if (Status != CommentStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending comments can be updated.");

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(
                    "Comment text is required.",
                    nameof(text));

            Text = text.Trim();

            MarkAsUpdated();
        }

        public void Approve()
        {
            if (Status == CommentStatus.Approved)
                return;

            Status = CommentStatus.Approved;

            MarkAsUpdated();
        }

        public void Reject()
        {
            if (Status == CommentStatus.Rejected)
                return;

            Status = CommentStatus.Rejected;

            MarkAsUpdated();
        }

        public void SetPending()
        {
            if (Status == CommentStatus.Pending)
                return;

            Status = CommentStatus.Pending;

            MarkAsUpdated();
        }

        public bool IsPending =>
            Status == CommentStatus.Pending;

        public bool IsApproved =>
            Status == CommentStatus.Approved;

        public bool IsRejected =>
            Status == CommentStatus.Rejected;
    }
}