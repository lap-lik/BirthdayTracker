﻿namespace Core.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Invalid password") { }
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
    }
}