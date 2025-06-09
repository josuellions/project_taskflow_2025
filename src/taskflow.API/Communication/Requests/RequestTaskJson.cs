using System.ComponentModel.DataAnnotations;
using taskflow.API.Enums;

namespace taskflow.API.Communication.Requests
{
    public class RequestTaskJson
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public Priority PriorityId { get; set; }
        
        [Required]
        public Status StatusId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public string Description { get; set; } = string.Empty ;
    }
}
