using System;

namespace SetStructure.CustomExceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message)
        {
            
        }
    }
}