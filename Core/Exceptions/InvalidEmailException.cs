namespace Core.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Invalid email") { }
        public InvalidEmailException(string message) : base(message) { }
        public InvalidEmailException(string message, Exception inner) : base(message, inner) { }
    }
}