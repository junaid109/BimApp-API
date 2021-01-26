using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BimApp.API.Services
{
    public class ApiExceptionService
    {
        public ApiExceptionService(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }
}
