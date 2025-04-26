using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_API.Controllers
{
    //for responsing from one file
    // generics means aita je kono datatype hote pare ,, declear kore kivabe <T> 
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        //  generice data type, is the means je kono type hote pare
        public T? Data { get; set; }
        // for list of error string stored 
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; }

        // now we will create constructor for Success or error code
        // success constructor;
        private ApiResponse(bool success, string message, T? data, List<string>? errors, int statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            TimeStamp = DateTime.UtcNow;
        }


        public static ApiResponse<T> SuccessResponse(T? data, int statusCode, string message = "") { return new ApiResponse<T>(true, message, data, null, statusCode); }

        // constructor for error response
        public static ApiResponse<T> ErrorResponse(List<string> errors, int statusCode, string message = "") { return new ApiResponse<T>(false, message, default(T), errors, statusCode); }

    }
}