using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Common;
using Shared.Helper.Pagination;


namespace Shared.Helper.Action_Result
{
    public static class ActionResultHelper
    {
        public static IActionResult HandleResponse<T>(Response<T> response)
        {
            return response.StatusCode switch
            {
                200 => new OkObjectResult(response), // OK: Successful retrieval
                400 => new BadRequestObjectResult(response), // Bad Request
                401 => new UnauthorizedObjectResult(response), // Unauthorized
                403 => new ObjectResult(response) { StatusCode = 403 }, // Forbidden
                404 => new NotFoundObjectResult(response), // Not Found
                405 => new ObjectResult(response) { StatusCode = 405 }, // Method Not Allowed
                406 => new ObjectResult(response) { StatusCode = 406 }, // Not Acceptable
                408 => new ObjectResult(response) { StatusCode = 408 }, // Request Timeout
                411 => new ObjectResult(response) { StatusCode = 411 }, // Length Required
                413 => new ObjectResult(response) { StatusCode = 413 }, // Payload Too Large
                415 => new ObjectResult(response) { StatusCode = 415 }, // Unsupported Media Type
                431 => new ObjectResult(response) { StatusCode = 431 }, // Request Header Fields Too Large
                502 => new ObjectResult(response) { StatusCode = 502 }, // Bad Gateway
                503 => new ObjectResult(response) { StatusCode = 503 }, // Service Unavailable
                501 => new ObjectResult(response) { StatusCode = 501 }, // Not Implemented
                _ => new ObjectResult(response) { StatusCode = 500 }, // Internal Server Error
            };
        }

        public static IActionResult HandlePaginationResponse<T>(PaginationResponse<PaginatedList<T>> response)
        {
            return response.StatusCode switch
            {
                200 => new OkObjectResult(response), // OK: Successful retrieval
                400 => new BadRequestObjectResult(response), // Bad Request
                401 => new UnauthorizedObjectResult(response), // Unauthorized
                403 => new ObjectResult(response) { StatusCode = 403 }, // Forbidden
                404 => new NotFoundObjectResult(response), // Not Found
                405 => new ObjectResult(response) { StatusCode = 405 }, // Method Not Allowed
                406 => new ObjectResult(response) { StatusCode = 406 }, // Not Acceptable
                408 => new ObjectResult(response) { StatusCode = 408 }, // Request Timeout
                411 => new ObjectResult(response) { StatusCode = 411 }, // Length Required
                413 => new ObjectResult(response) { StatusCode = 413 }, // Payload Too Large
                415 => new ObjectResult(response) { StatusCode = 415 }, // Unsupported Media Type
                431 => new ObjectResult(response) { StatusCode = 431 }, // Request Header Fields Too Large
                502 => new ObjectResult(response) { StatusCode = 502 }, // Bad Gateway
                503 => new ObjectResult(response) { StatusCode = 503 }, // Service Unavailable
                501 => new ObjectResult(response) { StatusCode = 501 }, // Not Implemented
                _ => new ObjectResult(response) { StatusCode = 500 }, // Internal Server Error
            };
        }


    }

}
