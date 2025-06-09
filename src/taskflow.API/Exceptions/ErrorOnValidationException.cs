namespace taskflow.API.Exceptions
{
    public class ErrorOnValidationException : TaskFlowInException
    {
        public ErrorOnValidationException(string message) : base(message)
        {

        }
    }
}
