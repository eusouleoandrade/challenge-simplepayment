using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }

        public AppException(IList<string> messageList) : base(String.Join(", ", messageList))
        {
        }

        public AppException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}