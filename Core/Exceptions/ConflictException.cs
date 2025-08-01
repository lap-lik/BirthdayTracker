﻿namespace Core.Exceptions
{ 
   public class ConflictException : Exception
    {
        public ConflictException() : base("Database operation failed") { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, Exception inner) : base(message, inner) { }
    }
}