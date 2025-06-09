using taskflow.API.Entities;

namespace taskflow.API.Communication.Responses
{
    public class ResponseReportJson
    {
        public int Total { get; set; } = 0;
        public int  Completed { get; set; } = 0;
        public decimal Porcentage { get; set; } = 0;
        
        public User? User { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public Project? Project { get; set; }

        public List<Tarefa>? Tasks { get; set; }
    }
}
