namespace taskflow.API.Exceptions
{
    public class NotFoundException : TaskFlowInException
    {

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
