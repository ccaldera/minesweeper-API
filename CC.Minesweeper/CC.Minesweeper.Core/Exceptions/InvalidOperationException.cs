using System;
using System.Runtime.Serialization;

namespace CC.Minesweeper.Core.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException() : base()
        {
        }

        public InvalidOperationException(string message) : base(message)
        {
        }

        public InvalidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
