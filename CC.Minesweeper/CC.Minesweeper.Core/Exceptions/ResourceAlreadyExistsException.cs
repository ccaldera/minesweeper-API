using System;
using System.Runtime.Serialization;

namespace CC.Minesweeper.Core.Exceptions
{
    /// <summary>
    /// The resource already exists exception.
    /// </summary>
    public class ResourceAlreadyExistsException : InvalidOperationException
    {
        /// <inheritdoc/>
        public ResourceAlreadyExistsException() : base()
        {
        }

        /// <inheritdoc/>
        public ResourceAlreadyExistsException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ResourceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
