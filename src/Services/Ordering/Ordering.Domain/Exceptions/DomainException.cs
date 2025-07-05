namespace Ordering.Domain.Exceptions
{
    public class DomainException   : Exception
    {
        public DomainException(string message, string? paramName = null)
            : base(message)
        {
            ParamName = paramName;
        }
        public string? ParamName { get; }
    }
    
}
