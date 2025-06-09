using System.ComponentModel.DataAnnotations;
using taskflow.API.Enums;

namespace taskflow.API.Communication.Requests
{
    public class RequestReportJson
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public UserType UserTypeId { get; set; }

        //[Required]
        public DateTime DateStart { get; set; } = DateTime.Now;

    }
}
