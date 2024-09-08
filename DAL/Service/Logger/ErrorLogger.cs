using DAL.Context.Control_Panel;
using Microsoft.AspNetCore.Http;
using Shared.Domain.Control_Panel.Administration.Logger;
using System.Text.Json;

namespace DAL.Service.Logger
{
    public class ErrorLogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ControlPanelDbContext _dbContext;

        // Define a Guid to represent unknown users
        private static readonly Guid UnknownUserId = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public ErrorLogger(IHttpContextAccessor httpContextAccessor, ControlPanelDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task LogErrorAsync(Exception ex)
        {
            var user = UserHelper.AppUser();

            var source = ex.Source ?? "Unknown";
            var methodName = ex.TargetSite?.Name ?? "Unknown";
            var parameters = ex.Data != null
                ? string.Join(", ", ex.Data.Keys.Cast<object>().Select(k => $"{k}: {ex.Data[k]}"))
                : "No parameters";

            // Initialize request-related variables
            var requestPath = string.Empty;
            var requestQueryString = string.Empty;
            var requestBody = string.Empty;
            var requestMethod = string.Empty;
            var requestHeaders = string.Empty;
            var requestIpAddress = string.Empty;

            // Additional machine details
            var machineName = Environment.MachineName;
            var osVersion = Environment.OSVersion.VersionString;
            var userDomainName = Environment.UserDomainName;
            var userNameOnPC = Environment.UserName;

            if (_httpContextAccessor?.HttpContext != null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                requestPath = httpContext.Request.Path;
                requestQueryString = httpContext.Request.QueryString.ToString();
                requestMethod = httpContext.Request.Method;
                requestHeaders = JsonSerializer.Serialize(httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
                requestIpAddress = httpContext.Connection.RemoteIpAddress?.ToString();

                if (httpContext.Request.Body != null && httpContext.Request.Body.CanSeek)
                {
                    httpContext.Request.Body.Position = 0;
                    using (var reader = new StreamReader(httpContext.Request.Body, leaveOpen: true))
                    {
                        requestBody = await reader.ReadToEndAsync();
                        httpContext.Request.Body.Position = 0; // Reset the stream position for further reads if necessary
                    }
                }
            }

            var errorLog = new ErrorLog
            {
                UserId = user?.Id ?? UnknownUserId,  // Use UnknownUserId if user is null
                UserName = user?.UserName ?? "Unknown", // Use "Unknown" if userName is null
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                Source = source,
                MethodName = methodName,
                Parameters = parameters,
                RequestPath = requestPath,
                RequestQueryString = requestQueryString,
                RequestBody = requestBody,
                RequestMethod = requestMethod,
                RequestHeaders = requestHeaders,
                RequestIpAddress = requestIpAddress,
                MachineName = machineName,
                OsVersion = osVersion,
                UserDomainName = userDomainName,
                UserNameOnPC = userNameOnPC,
                ExceptionType = ex.GetType().FullName,
                InnerExceptionMessage = ex.InnerException?.Message,
                InnerExceptionStackTrace = ex.InnerException?.StackTrace,
                Timestamp = DateTime.Now
            };

            try
            {
                _dbContext.Set<ErrorLog>().Add(errorLog);
                 await _dbContext.SaveChangesAsync(); // Save the error log
            }
            catch (Exception saveEx)
            {
                // Handle additional error logging failure (e.g., log to a file or external system)
                Console.WriteLine($"Error logging failed: {saveEx.Message}");
                // Log this failure or handle it as per your application's requirements
            }
        }
    }
}
