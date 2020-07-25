using System;
using System.IO;

namespace HSAEnrollmentApplication
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Response (bool success, string message, DateTime date)
        {
            Success = success;
            Message = message;
            Date = date;
        }
    }
}
