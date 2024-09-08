using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Domain.Control_Panel.Administration.Logger;
using System.Net.NetworkInformation;
using System.Text.Json;


namespace DAL.Service.Logger
{
    public static class StoreActivityLogger<TContext> where TContext : DbContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }


        public static async Task LogAddedActivityAsync(TContext dbContext, EntityEntry entry)
        {
            if (entry.Entity.GetType() == typeof(UserLoginInformation))
            {
                // Skip logging for UserLoginInformation entity
                return;
            }

            if (entry.Entity.GetType() == typeof(ErrorLog))
            {
                // Skip logging for UserLoginInformation entity
                return;
            }

            var action = "Added";
            var entity = entry.Entity;
            var entityType = entity.GetType().Name;
            var entityId = string.Empty;
            var addedValues = new Dictionary<string, string>();

            // After insertion, retrieve the entity from the database to get all data
            var primaryKey = dbContext.Entry(entity).Metadata.FindPrimaryKey();
            var primaryKeyValues = primaryKey.Properties.Select(p => dbContext.Entry(entity).Property(p.Name).CurrentValue);
            entityId = string.Join(", ", primaryKeyValues);

            var dbEntity = await dbContext.FindAsync(entity.GetType(), primaryKeyValues.ToArray());
            if (dbEntity != null)
            {
                // Capture all property values
                var properties = dbEntity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(dbEntity)?.ToString();
                    addedValues.Add(propertyName, propertyValue);
                }
            }

            var user = UserHelper.AppUser();
            if (user == null)
            {
                throw new InvalidOperationException("User is not logged in. Please login to perform this operation.");
            }

            var httpContext = _httpContextAccessor?.HttpContext;
            var requestIpAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
            var requestUrl = httpContext?.Request.Path.ToString();

            // Get MAC address and IP address
            var (macAddress, ipAddress) = GetMacAddress();

            dbContext.Set<ActivityLog>().Add(new ActivityLog
            {
                UserLoginInformationId = user.UserLoginInformationId,
                UserId = user?.Id,
                UserName = user.UserName,
                ActionType = action,
                EntityType = entityType,
                EntityId = entityId,
                AddedValues = JsonSerializer.Serialize(addedValues),
                Timestamp = DateTime.UtcNow,
                OrganizationId = user.OrganizationId,
                CompanyId = user.CompanyId,
                RequestIpAddress = requestIpAddress,
                RequestUrl = requestUrl,
                DbContextName = dbContext.GetType().Name,
                MachineName = Environment.MachineName,
                OsVersion = Environment.OSVersion.ToString(),
                UserDomainName = Environment.UserDomainName,
                UserNameOnPC = Environment.UserName,
                MacAddress = macAddress,
                IpAddress = ipAddress
            });
        }

        public static void LogModifiedActivityAsync(TContext dbContext, EntityEntry entry)
        {
            if (entry.State == EntityState.Modified)
            {
                var action = "Modified";
                var entity = entry.Entity;
                var entityType = entity.GetType().Name;
                var entityId = GetEntityId(entry);

                var (previousValues, presentValues, propertyChanges, previousPropertyValues, changesPropertyValues) = GetModifiedPropertyValues(entry);

                var user = UserHelper.AppUser();
                if (user == null)
                {
                    throw new InvalidOperationException("User is not logged in. Please login to perform this operation.");
                }

                var httpContext = _httpContextAccessor?.HttpContext;
                var requestIpAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
                var requestUrl = httpContext?.Request.Path.ToString();

                // Get MAC address and IP address
                var (macAddress, ipAddress) = GetMacAddress();

                dbContext.Set<ActivityLog>().Add(new ActivityLog
                {
                    UserLoginInformationId = user.UserLoginInformationId,
                    UserId = user.Id,
                    UserName = user.UserName,
                    ActionType = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    PreviousValues = JsonSerializer.Serialize(previousValues),
                    PresentValues = JsonSerializer.Serialize(presentValues),
                    PropertyChanges = JsonSerializer.Serialize(propertyChanges),
                    PreviousPropertyValues = JsonSerializer.Serialize(previousPropertyValues),
                    ChangesPropertyValues = JsonSerializer.Serialize(changesPropertyValues),
                    Timestamp = DateTime.UtcNow,
                    OrganizationId = user.OrganizationId,
                    CompanyId = user.CompanyId,
                    RequestIpAddress = requestIpAddress,
                    RequestUrl = requestUrl,
                    DbContextName = dbContext.GetType().Name,
                    MachineName = Environment.MachineName,
                    OsVersion = Environment.OSVersion.ToString(),
                    UserDomainName = Environment.UserDomainName,
                    UserNameOnPC = Environment.UserName,
                    MacAddress = macAddress,
                    IpAddress = ipAddress
                });
            }
        }

        public static void LogDeletedActivityAsync(TContext dbContext, EntityEntry entry)
        {
            if (entry.State == EntityState.Deleted)
            {
                var action = "Deleted";
                var entity = entry.Entity;
                var entityType = entity.GetType().Name;
                var entityId = GetEntityId(entry);
                var previousValues = GetOriginalPropertyValues(entry);

                var user = UserHelper.AppUser();
                if (user == null)
                {
                    throw new InvalidOperationException("User is not logged in. Please login to perform this operation.");
                }

                var httpContext = _httpContextAccessor?.HttpContext;
                var requestIpAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
                var requestUrl = httpContext?.Request.Path.ToString();

                // Get MAC address and IP address
                var (macAddress, ipAddress) = GetMacAddress();

                dbContext.Set<ActivityLog>().Add(new ActivityLog
                {
                    UserLoginInformationId = user.UserLoginInformationId,
                    UserId = user.Id,
                    UserName = user.UserName,
                    ActionType = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    PreviousValues = JsonSerializer.Serialize(previousValues),
                    Timestamp = DateTime.UtcNow,
                    OrganizationId = user.OrganizationId,
                    CompanyId = user.CompanyId,
                    RequestIpAddress = requestIpAddress,
                    RequestUrl = requestUrl,
                    DbContextName = dbContext.GetType().Name,
                    MachineName = Environment.MachineName,
                    OsVersion = Environment.OSVersion.ToString(),
                    UserDomainName = Environment.UserDomainName,
                    UserNameOnPC = Environment.UserName,
                    MacAddress = macAddress,
                    IpAddress = ipAddress
                });
            }
        }

        private static string GetEntityId(EntityEntry entry)
        {
            var primaryKey = entry.Metadata.FindPrimaryKey();
            if (primaryKey != null)
            {
                var primaryKeyValues = primaryKey.Properties.Select(p => entry.Property(p.Name).CurrentValue);
                return string.Join(", ", primaryKeyValues);
            }
            return string.Empty;
        }

        private static Dictionary<string, string> GetOriginalPropertyValues(EntityEntry entry)
        {
            var previousValues = new Dictionary<string, string>();

            foreach (var property in entry.OriginalValues.Properties)
            {
                var propertyName = property.Name;
                var originalValue = entry.OriginalValues[property]?.ToString();
                previousValues.Add(propertyName, originalValue);
            }

            return previousValues;
        }

        private static (Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>) GetModifiedPropertyValues(EntityEntry entry)
        {
            var previousValues = new Dictionary<string, string>();
            var presentValues = new Dictionary<string, string>();
            var propertyChanges = new Dictionary<string, string>();
            var previousPropertyValues = new Dictionary<string, string>();
            var changesPropertyValues = new Dictionary<string, string>();

            foreach (var property in entry.OriginalValues.Properties)
            {
                var propertyName = property.Name;
                var originalValue = entry.OriginalValues[property]?.ToString();
                var currentValue = entry.CurrentValues[property]?.ToString();

                if (originalValue != currentValue)
                {
                    previousValues.Add(propertyName, originalValue);
                    presentValues.Add(propertyName, currentValue);
                    propertyChanges.Add(propertyName, $"Changed from {originalValue} to {currentValue}");
                    previousPropertyValues.Add(propertyName, originalValue);
                    changesPropertyValues.Add(propertyName, currentValue);
                }
            }

            return (previousValues, presentValues, propertyChanges, previousPropertyValues, changesPropertyValues);
        }



        public static (string macAddress, string ipAddress) GetMacAddress()
        {
            var macAddressesList = new List<string>();
            var ipAddressesList = new List<string>();

            try
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .ToList();

                foreach (var networkInterface in networkInterfaces)
                {
                    var physicalAddress = networkInterface.GetPhysicalAddress();
                    if (physicalAddress != null && physicalAddress.ToString() != "")
                    {
                        macAddressesList.Add(physicalAddress.ToString());
                        var properties = networkInterface.GetIPProperties();
                        var unicastAddresses = properties.UnicastAddresses;
                        if (unicastAddresses.Any())
                        {
                            ipAddressesList.Add(unicastAddresses.First().Address.ToString());
                        }
                    }
                }

                if (macAddressesList.Count == 0)
                {
                    Console.WriteLine("No MAC address found.");
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception appropriately
                Console.WriteLine($"An error occurred while retrieving the MAC addresses: {ex.Message}");
            }

            // Join lists into comma-separated strings
            var macAddresses = string.Join(", ", macAddressesList);
            var ipAddresses = string.Join(", ", ipAddressesList);

            return (macAddresses, ipAddresses);
        }



    }
}
