using System;
using System.IO;

namespace HSAEnrollmentApplication
{
    public class Response
    {
        public bool Success { get; set; }
        public MemoryStream Stream { get; set; }
        public string Message { get; set; }

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Response(bool success, string message, MemoryStream stream)
        {
            Success = success;
            Message = message;
            Stream = stream;
        }
    }
}
