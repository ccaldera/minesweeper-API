using System;
using System.Runtime.Serialization;

namespace CC.Minesweeper.Core.Exceptions
{
    public class ResourceAlreadyExistsException : InvalidOperationException
    {
        public ResourceAlreadyExistsException() : base()
        {
        }

        public ResourceAlreadyExistsException(string message) : base(message)
        {
        }

        public ResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
