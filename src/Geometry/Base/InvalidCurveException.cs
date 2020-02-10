
using System;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Exception for invalid curve
    /// </summary>
    public class InvalidCurveException : Exception
    {
        /// <inheritdoc/>
        public InvalidCurveException()
        {
        }
        /// <inheritdoc/>
        public InvalidCurveException(string message) : base(message)
        {
        }
        /// <inheritdoc/>
        public InvalidCurveException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }

}