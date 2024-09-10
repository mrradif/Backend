using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security;

public static class ExceptionHelper
{
    public static int MapExceptionToStatusCode(Exception ex)
    {
        var exceptionStatusCodeMapping = new Dictionary<Type, int>
        {
            // Client Errors
            { typeof(ArgumentNullException), 400 }, // Bad Request
            { typeof(ArgumentException), 400 }, // Bad Request
            { typeof(FormatException), 400 }, // Bad Request
            { typeof(IndexOutOfRangeException), 400 }, // Bad Request
            { typeof(InvalidDataException), 400 }, // Bad Request
            { typeof(OverflowException), 400 }, // Bad Request
            { typeof(EndOfStreamException), 400 }, // Bad Request
            { typeof(ArithmeticException), 400 }, // Bad Request
            { typeof(ArgumentOutOfRangeException), 400 }, // Bad Request

            // Validation Errors (Data Annotations)
            { typeof(ValidationException), 400 }, // Bad Request - For data annotations like [Required], [StringLength], etc.

            // Unauthorized and Authentication Errors
            { typeof(UnauthorizedAccessException), 401 }, // Unauthorized
            { typeof(AuthenticationException), 401 }, // Unauthorized
            { typeof(SecurityException), 403 }, // Forbidden
            { typeof(AccessViolationException), 403 }, // Forbidden

            // Not Found Errors
            { typeof(KeyNotFoundException), 404 }, // Not Found
            { typeof(HttpRequestException), 404 }, // Not Found
            { typeof(DirectoryNotFoundException), 404 }, // Not Found
            { typeof(FileNotFoundException), 404 }, // Not Found

            // Method Not Allowed
            { typeof(MethodAccessException), 405 }, // Method Not Allowed

            // Request Timeout
            { typeof(TimeoutException), 408 }, // Request Timeout
            { typeof(OperationCanceledException), 408 }, // Request Timeout
            { typeof(TaskCanceledException), 408 }, // Request Timeout

            // Payload Too Large
            { typeof(FileLoadException), 413 }, // Payload Too Large

            // Unsupported Media Type
            { typeof(InvalidCastException), 415 }, // Unsupported Media Type

            // Request Header Fields Too Large
            { typeof(InsufficientMemoryException), 431 }, // Request Header Fields Too Large

            // Server Errors
            { typeof(BadImageFormatException), 502 }, // Bad Gateway
            { typeof(NotImplementedException), 501 }, // Not Implemented
            { typeof(ObjectDisposedException), 503 }, // Service Unavailable
            { typeof(IOException), 500 }, // Internal Server Error
            { typeof(CryptographicException), 500 }, // Internal Server Error
            { typeof(SerializationException), 500 }, // Internal Server Error
            { typeof(InvalidOperationException), 500 }, // Internal Server Error
            { typeof(OutOfMemoryException), 500 }, // Internal Server Error
            { typeof(InvalidProgramException), 500 }, // Internal Server Error
            { typeof(AggregateException), 500 }, // Internal Server Error
            { typeof(ProtocolViolationException), 500 }, // Internal Server Error
            { typeof(NotSupportedException), 500 }, // Internal Server Error

            // Default case for any unhandled exception
            { typeof(Exception), 500 } // Internal Server Error
        };

        // Get status code or default to 500
        return exceptionStatusCodeMapping.TryGetValue(ex.GetType(), out var statusCode)
            ? statusCode
            : 500; // Internal Server Error for unhandled exceptions
    }
}
