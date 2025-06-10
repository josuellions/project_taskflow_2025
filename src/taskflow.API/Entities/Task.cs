using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using taskflow.API.Enums;

namespace taskflow.API.Entities
{
    [Table("Tarefas")]
    public class Tarefa
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public Status StatusId { get; set; }

        [Required]
        public Priority PriorityId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public DateTime DateAt { get; set; } = DateTime.UtcNow;
        
        public DateTime DateUp { get; set; } = DateTime.UtcNow;
        
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
