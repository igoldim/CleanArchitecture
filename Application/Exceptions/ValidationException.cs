using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    internal class ValidationException : Exception
    {
        private Dictionary<string, string[]> errorsDictionary;

        public ValidationException()
        {
        }

        public ValidationException(Dictionary<string, string[]> errorsDictionary)
        {
            this.errorsDictionary = errorsDictionary;
        }

        public ValidationException(string? message) : base(message)
        {
        }

        public ValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}