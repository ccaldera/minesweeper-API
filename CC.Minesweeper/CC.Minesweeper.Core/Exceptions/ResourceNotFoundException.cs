using System;
using System.Runtime.Serialization;

namespace CC.Minesweeper.Core.Exceptions
{
    /// <summary>
    /// The recource not found exception.
    /// </summary>
    public class ResourceNotFoundException : InvalidOperationException
    {
        /// <inheritdoc/>
        public ResourceNotFoundException() : base()
        {
        }

        /// <inheritdoc/>
        public ResourceNotFoundException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
