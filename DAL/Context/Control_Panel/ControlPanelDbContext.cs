using DAL.Logger;
using DAL.Service.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.Domain.Control_Panel.Administration.Logger;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.Domain.Control_Panel.Identity;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;


namespace DAL.Context.Control_Panel
{
    public class ControlPanelDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public ControlPanelDbContext(DbContextOptions<ControlPanelDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // User Login Inforamtions
        public DbSet<UserLoginInformation> tblUserLoginInformations { get; set; }

        // Activity Logs
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }


        // Org Config
        public DbSet<Organization> tblOrganizations { get; set; }
        public DbSet<Company> tblCompanies { get; set; }
        public DbSet<Division> tblDivisions { get; set; }
        public DbSet<Branch> tblBranches { get; set; }


        // Config Table
        public DbSet<OrganizationPolicyConfig> tblOrganizationPolicyConfigs { get; set; }
        public DbSet<CompanyPolicyConfig> tblCompanyPolicyConfigs { get; set; }
        public DbSet<UserCreationConfig> tblUserCreationConfigs { get; set; }


        // App Config
        public DbSet<Application> tblApplications { get; set; }
        public DbSet<Module> tblModules { get; set; }
        public DbSet<MainMenu> tblMainMenus { get; set; }
        public DbSet<SubMenu> tblSubMenus { get; set; }
        public DbSet<PageTab> tblPageTabs { get; set; }



        public DbSet<Component> tblComponents { get; set; }
        public DbSet<Button> tblButtons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>()
                .HasIndex(o => o.OrgUniqueId)
                .IsUnique()
                .HasAnnotation("ErrorMessage", "The OrgUniqueId must be unique.");

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.ComUniqueId)
                .IsUnique()
                .HasAnnotation("ErrorMessage", "The ComUniqueId must be unique.");

        }





        // ....................................................................
        // ..........................................Activity Logs
        // .............................. Starting
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    try
        //    {
        //        if (ActivityServiceOnOff.UseActivityLogService)
        //        {
        //            foreach (var entry in ChangeTracker.Entries().ToList())
        //            {
        //                switch (entry.State)
        //                {
        //                    case EntityState.Added:
        //                        await StoreActivityLogger<ControlPanelDbContext>.LogAddedActivityAsync(this, entry);
        //                        break;
        //                    case EntityState.Modified:
        //                        StoreActivityLogger<ControlPanelDbContext>.LogModifiedActivityAsync(this, entry);
        //                        break;
        //                    case EntityState.Deleted:
        //                        StoreActivityLogger<ControlPanelDbContext>.LogDeletedActivityAsync(this, entry);
        //                        break;
        //                }
        //            }
        //        }

        //        var result = await base.SaveChangesAsync(cancellationToken);


        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        // Log error using ErrorLogger
        //        //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);


        //        throw; 
        //    }

        //}




        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{

        //    // Create a copy of the collection to avoid modifying it while iterating
        //    foreach (var entry in ChangeTracker.Entries().ToList())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                await LogActivityAsync(entry);
        //                break;
        //            case EntityState.Modified:
        //                LogModifiedActivity();
        //                break;
        //            case EntityState.Deleted:
        //                LogDeletedActivity();
        //                break;
        //        }
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}



        private async Task LogActivityAsync(EntityEntry entri)
        {
            if (entri.Entity.GetType() == typeof(UserLoginInformation))
            {
                // Skip logging for UserLoginInformation entity
                return;
            }

            if (entri.Entity.GetType() == typeof(ErrorLog))
            {
                // Skip logging for UserLoginInformation entity
                return;
            }

            var addedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();

            var dbContextName = nameof(ControlPanelDbContext);

            foreach (var entry in addedEntries)
            {
                var action = "Added"; // For added entities, set the action to "Added"
                var entity = entry.Entity;
                var entityType = entity.GetType().Name;
                var entityId = string.Empty;
                var addedValues = new Dictionary<string, string>();

                // Save the changes to the database to obtain the entity's ID
                await base.SaveChangesAsync();

                // After insertion, retrieve the entity from the database to get all data
                var primaryKey = Entry(entity).Metadata.FindPrimaryKey();
                var primaryKeyValues = primaryKey.Properties.Select(p => Entry(entity).Property(p.Name).CurrentValue);
                entityId = string.Join(", ", primaryKeyValues);

                // After insertion, retrieve the entity from the database to get all data
                var dbEntity = await base.FindAsync(entity.GetType(), primaryKeyValues.ToArray());
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


                // Insert activity log for the added entity
                ActivityLogs.Add(new ActivityLog
                {
                    UserId = user.Id, // You need to replace this with the actual user ID
                    ActionType = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    AddedValues = JsonSerializer.Serialize(addedValues), // Include added values in the activity log
                    Timestamp = DateTime.Now,
                    UserLoginInformationId = user.UserLoginInformationId,
                    UserName = user.UserName,
            
                    OrganizationId = user.OrganizationId,
                    CompanyId = user.CompanyId,
                    RequestIpAddress = requestIpAddress,
                    RequestUrl = requestUrl,
                    DbContextName = dbContextName,
                    MachineName = Environment.MachineName,
                    OsVersion = Environment.OSVersion.ToString(),
                    UserDomainName = Environment.UserDomainName,
                    UserNameOnPC = Environment.UserName,
                    MacAddress = macAddress,
                    IpAddress = ipAddress
                });
            }

            // Save changes to the database
            await base.SaveChangesAsync();
        }


        public (string macAddress, string ipAddress) GetMacAddress()
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



        private void LogModifiedActivity()
        {
            var dbContextName = nameof(ControlPanelDbContext);
            foreach (var entry in ChangeTracker.Entries().ToList())
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


                    // Insert activity log for the modified entity
                    ActivityLogs.Add(new ActivityLog
                    {
                        UserId = user.Id,
                        ActionType = action,
                        EntityType = entityType,
                        EntityId = entityId,
                        PreviousValues = JsonSerializer.Serialize(previousValues),
                        PresentValues = JsonSerializer.Serialize(presentValues),
                        PropertyChanges = JsonSerializer.Serialize(propertyChanges),
                        PreviousPropertyValues = JsonSerializer.Serialize(previousPropertyValues),
                        ChangesPropertyValues = JsonSerializer.Serialize(changesPropertyValues),
                        Timestamp = DateTime.UtcNow,
                        UserLoginInformationId = user.UserLoginInformationId,
                        UserName = user.UserName,

                        OrganizationId = user.OrganizationId,
                        CompanyId = user.CompanyId,
                        RequestIpAddress = requestIpAddress,
                        RequestUrl = requestUrl,
                        DbContextName = dbContextName,
                        MachineName = Environment.MachineName,
                        OsVersion = Environment.OSVersion.ToString(),
                        UserDomainName = Environment.UserDomainName,
                        UserNameOnPC = Environment.UserName,
                        MacAddress = macAddress,
                        IpAddress = ipAddress
                    });
                }
            }

            // Save changes to the database
            base.SaveChanges();
        }

        private void LogDeletedActivity()
        {
            var dbContextName = nameof(ControlPanelDbContext);

            foreach (var entry in ChangeTracker.Entries().ToList())
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

                    // Insert activity log for the deleted entity
                    ActivityLogs.Add(new ActivityLog
                    {
                        UserId = user.Id,
                        ActionType = action,
                        EntityType = entityType,
                        EntityId = entityId,
                        PreviousValues = JsonSerializer.Serialize(previousValues),
                        Timestamp = DateTime.UtcNow,

                        UserLoginInformationId = user.UserLoginInformationId,
                        UserName = user.UserName,

                        OrganizationId = user.OrganizationId,
                        CompanyId = user.CompanyId,
                        RequestIpAddress = requestIpAddress,
                        RequestUrl = requestUrl,
                        DbContextName = dbContextName,
                        MachineName = Environment.MachineName,
                        OsVersion = Environment.OSVersion.ToString(),
                        UserDomainName = Environment.UserDomainName,
                        UserNameOnPC = Environment.UserName,
                        MacAddress = macAddress,
                        IpAddress = ipAddress
                    });
                }
            }

            // Save changes to the database
            base.SaveChanges();
        }

        private string GetEntityId(EntityEntry entry)
        {
            var primaryKey = entry.Metadata.FindPrimaryKey();
            if (primaryKey != null)
            {
                var primaryKeyValues = primaryKey.Properties.Select(p => entry.Property(p.Name).CurrentValue);
                return string.Join(", ", primaryKeyValues);
            }
            return string.Empty;
        }

        private Dictionary<string, string> GetOriginalPropertyValues(EntityEntry entry)
        {
            var previousValues = new Dictionary<string, string>();

            // Capture previous values for all properties
            foreach (var property in entry.OriginalValues.Properties)
            {
                var propertyName = property.Name;
                var originalValue = entry.OriginalValues[property]?.ToString();
                previousValues.Add(propertyName, originalValue);
            }

            return previousValues;
        }

        private (Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>, Dictionary<string, string>) GetModifiedPropertyValues(EntityEntry entry)
        {
            var entity = entry.Entity;
            var previousValues = new Dictionary<string, string>();
            var presentValues = new Dictionary<string, string>();
            var propertyChanges = new Dictionary<string, string>();
            var previousPropertyValues = new Dictionary<string, string>();
            var changesPropertyValues = new Dictionary<string, string>();

            // Get the primary key value if available
            var primaryKey = entry.Metadata.FindPrimaryKey();
            var entityId = primaryKey != null ? GetEntityId(entry) : string.Empty;

            // Create a copy of the properties collection to avoid modification while iterating
            var properties = entry.Properties.ToList();
            var databaseValues = entry.GetDatabaseValues();
            foreach (var property in properties)
            {
                var propertyName = property.Metadata.Name;
                var originalValue = databaseValues?[propertyName]?.ToString();
                var currentValue = entry.Property(propertyName).CurrentValue?.ToString();

                previousValues.Add(propertyName, originalValue);
                presentValues.Add(propertyName, currentValue);

                if (entry.Property(propertyName).IsModified && originalValue != currentValue)
                {
                    propertyChanges.Add(propertyName, $"{originalValue}, {currentValue}");
                    previousPropertyValues.Add(propertyName, originalValue);
                    changesPropertyValues.Add(propertyName, currentValue);
                }
            }

            return (previousValues, presentValues, propertyChanges, previousPropertyValues, changesPropertyValues);
        }



        // ....................................................................
        // ..........................................Activity Logs
        // .............................. End




        // ....................................................................
        // ..........................................Token Validation
        // .............................. Starting

        public async Task<(bool access, bool logout)> GetLogoutTimeAndAccessForUserAsync(string username, string userAgent, string token)
        {
            // Check if there exists any entry with matching criteria
            var userLoginInfo = await tblUserLoginInformations
                .Where(u => u.UserName == username && u.UserAgent == userAgent && u.Token == token)
                .OrderByDescending(u => u.LogoutTime)
                .FirstOrDefaultAsync();

            if (userLoginInfo != null)
            {
                // Return access true and logout false if logout time is not set
                if (userLoginInfo.LogoutTime == null)
                {
                    return (access: true, logout: false);
                }
                // Return access true and logout true if logout time is set
                else
                {
                    return (access: true, logout: true);
                }
            }

            // Return access false and logout false if no matching record found
            return (access: false, logout: false);
        }

        // ....................................................................
        // ..........................................Token Validation
        // .............................. End

    }
}
