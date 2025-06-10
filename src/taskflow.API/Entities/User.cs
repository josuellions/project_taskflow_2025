using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using taskflow.API.Enums;

namespace taskflow.API.Entities
{
    [Table("Usuarios")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public UserType TypeId{ get; set; }

        public DateTime DateAt { get; set; } = DateTime.UtcNow;
        
        public DateTime DateUp { get; set; } = DateTime.UtcNow;
    }
}
