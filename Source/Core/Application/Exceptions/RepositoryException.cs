using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class RepositoryException : AppException
    {
        // Fields
        private const string _message = "Failed to process data store";

        // Ctors
        public RepositoryException() : this(_message)
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(IList<string> messageList) : base(messageList)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RepositoryException(System.Exception innerException) : base(_message, innerException)
        {
        }
    }
}