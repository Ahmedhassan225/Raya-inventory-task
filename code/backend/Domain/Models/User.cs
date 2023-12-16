using Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? MobilePhone { get; set; }
        public Nullable<bool> Activated { get; set; }

    }
}
