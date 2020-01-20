using System;

namespace Billas.Identifier
{
    public class PersonIdentifierFormatException : FormatException
    {
        public string Value { get; }
        
        public PersonIdentifierFormatException(string value, string message)
            : this(value, message, null) { }

        public PersonIdentifierFormatException(string value, string message, Exception innerException)
            : base(message, innerException)
        {
            Value = value;
        }
    }
}
