using System;

namespace Task1.CustomExceptions
{
    public class TypeIsNotRegisteredExeption : Exception
    {
        public TypeIsNotRegisteredExeption(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}