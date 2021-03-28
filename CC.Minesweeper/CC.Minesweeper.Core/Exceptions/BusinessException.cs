using System;
using System.Runtime.Serialization;

namespace CC.Minesweeper.Core.Exceptions
{
    /// <summary>
    /// The business rule exception.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <inheritdoc/>
        public BusinessException() : base()
        {
        }

        /// <inheritdoc/>
        public BusinessException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
