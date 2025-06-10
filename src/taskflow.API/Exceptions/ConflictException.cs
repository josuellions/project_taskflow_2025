namespace taskflow.API.Exceptions
{
    public class ConflictException : TaskFlowInException
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}
