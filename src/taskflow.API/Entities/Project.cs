using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using taskflow.API.Enums;

namespace taskflow.API.Entities
{
    [Table("Projetos")]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Status StatusId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime DataAt { get; set; } = DateTime.Now;
        
        public DateTime DataUp { get; set; } = DateTime.Now;
        
        public ICollection<Tarefa> Tasks { get; set; } = new List<Tarefa>();
    }
}
