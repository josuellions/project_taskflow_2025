using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using taskflow.API.Enums;

namespace taskflow.API.Entities
{
    [Table("TarefasHistoricos")]
    public class TaskHistory
    {
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Required]
        public string? TaskDescription { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public Status StatusId { get; set; }

        [Required]
        public Actions ActionId { get; set; }

        public DateTime DateAt { get; set; }

        public string? Description { get; set; }
    }
}
