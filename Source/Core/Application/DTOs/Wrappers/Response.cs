using System.Collections.Generic;

namespace Application.DTOs.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
    }

    public class Response
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public Response()
        {
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(IEnumerable<string> errors)
        {
            Message = "Failed to process request";
            Succeeded = false;
            Errors = errors;
        }
    }
}