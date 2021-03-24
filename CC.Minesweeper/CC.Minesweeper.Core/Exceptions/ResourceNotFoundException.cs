using System;
using System.Runtime.Serialization;

namespace CC.Minesweeper.Core.Exceptions
{
    public class ResourceNotFoundException : InvalidOperationException
    {
        public ResourceNotFoundException() : base()
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
