using Charbagh.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Charbagh.Domain.Common
{
    public abstract class PersonProfile : BaseEntity
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; protected set; }

        public string? Notes { get; protected set; }

        public User User { get; protected set; } = null!;
    }
}