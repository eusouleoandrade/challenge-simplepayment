using System.Collections.Generic;

namespace Application.DTOs.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public T Data { get; set; }

        public Response()
        {
            Errors = new List<string>();
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message ?? "Request processed";
            Data = data;
            Errors = new List<string>();
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            Errors = new List<string>();
        }
    }

    public class Response
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public Response()
        {
            Errors = new List<string>();
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            Errors = new List<string>();
        }

        public Response(IEnumerable<string> errors)
        {
            Message = "Failed to process request";
            Succeeded = false;
            Errors = errors;
        }
    }
}