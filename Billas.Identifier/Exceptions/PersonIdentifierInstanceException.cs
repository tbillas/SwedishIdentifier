using System;

namespace Billas.Identifier
{
    public class PersonIdentifierInstanceException : SystemException
    {
        public PersonIdentifierInstanceException(string message) 
            : base(message) { }

        public PersonIdentifierInstanceException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}