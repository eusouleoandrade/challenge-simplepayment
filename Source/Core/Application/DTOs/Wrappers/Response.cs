using System.Collections.Generic;
using Newtonsoft.Json;

namespace Application.DTOs.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Errors { get; set; }

        public T Data { get; set; }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message ?? "Request processed";
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
        private const string succeededMessage = "Request processed";

        private const string failedMessage = "Failed to process request";

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
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

        public Response(bool succeeded, string message = null)
        {
            Succeeded = succeeded;
            Message = message ?? succeededMessage;
        }

        public Response(bool succeeded)
        {
            Succeeded = succeeded;
            Message = succeeded ? succeededMessage : failedMessage;
            Errors = new List<string>();
        }

        public Response(IEnumerable<string> errors)
        {
            Message = failedMessage;
            Succeeded = false;
            Errors = errors;
        }
    }
}