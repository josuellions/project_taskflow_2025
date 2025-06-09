using System.ComponentModel.DataAnnotations;
using taskflow.API.Enums;

namespace taskflow.API.Communication.Requests
{
    public class RequestProjectJson
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Status StatusId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
