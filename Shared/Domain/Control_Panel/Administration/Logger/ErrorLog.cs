namespace Shared.Domain.Control_Panel.Administration.Logger
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string MethodName { get; set; }
        public string Parameters { get; set; }
        public string RequestPath { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestBody { get; set; }
        public string RequestMethod { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestIpAddress { get; set; }
        public string MachineName { get; set; }
        public string OsVersion { get; set; }
        public string UserDomainName { get; set; }
        public string UserNameOnPC { get; set; }
        public string RequestUrl { get; set; }
        public string DbContextName { get; set; }
        public string ActionType { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string ExceptionType { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string InnerExceptionStackTrace { get; set; }
        public DateTime Timestamp { get; set; }
    }


}
