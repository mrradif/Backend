using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Control_Panel.Administration.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DAL.Service.Logger
{
    public class EntityChangeLogger<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public EntityChangeLogger(TContext context)
        {
            _context = context;
        }

        public async Task LogChangesAsync()
        {
            var changeTracker = _context.ChangeTracker;
            var entries = changeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        await LogAddedActivityAsync(entry);
                        break;
                    case EntityState.Modified:
                        LogModifiedActivity(entry);
                        break;
                    case EntityState.Deleted:
                        LogDeletedActivity(entry);
                        break;
                }
            }
        }

        private async Task LogAddedActivityAsync(EntityEntry entry)
        {
            var action = "Added";
            var entity = entry.Entity;
            var entityType = entity.GetType().Name;
            var entityId = string.Empty;
            var addedValues = new Dictionary<string, string>();

            await _context.SaveChangesAsync();

            var primaryKey = entry.Metadata.FindPrimaryKey();
            var primaryKeyValues = primaryKey.Properties.Select(p => entry.Property(p.Name).CurrentValue);
            entityId = string.Join(", ", primaryKeyValues);

            var dbEntity = await _context.FindAsync(entity.GetType(), primaryKeyValues.ToArray());
            if (dbEntity != null)
            {
                var properties = dbEntity.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(dbEntity)?.ToString();
                    addedValues.Add(propertyName, propertyValue);
                }
            }

            var user = UserHelper.AppUser();

            _context.Set<ActivityLog>().Add(new ActivityLog
            {
                UserId = user.Id,
                ActionType = action,
                EntityType = entityType,
                EntityId = entityId,
                AddedValues = JsonSerializer.Serialize(addedValues),
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        private void LogModifiedActivity(EntityEntry entry)
        {
            var action = "Modified";
            var entity = entry.Entity;
            var entityType = entity.GetType().Name;
            var entityId = GetEntityId(entry);
            var (previousValues, presentValues, propertyChanges, previousPropertyValues, changesPropertyValues) = GetModifiedPropertyValues(entry);

            var user = UserHelper.AppUser();

            _context.Set<ActivityLog>().Add(new ActivityLog
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
                Timestamp = DateTime.UtcNow
            });

            _context.SaveChanges();
        }

        private void LogDeletedActivity(EntityEntry entry)
        {
            var action = "Deleted";
            var entity = entry.Entity;
            var entityType = entity.GetType().Name;
            var entityId = GetEntityId(entry);
            var previousValues = GetOriginalPropertyValues(entry);

            var user = UserHelper.AppUser();

            _context.Set<ActivityLog>().Add(new ActivityLog
            {
                UserId = user.Id,
                ActionType = action,
                EntityType = entityType,
                EntityId = entityId,
                PreviousValues = JsonSerializer.Serialize(previousValues),
                Timestamp = DateTime.UtcNow
            });

            _context.SaveChanges();
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
            var previousValues = new Dictionary<string, string>();
            var presentValues = new Dictionary<string, string>();
            var propertyChanges = new Dictionary<string, string>();
            var previousPropertyValues = new Dictionary<string, string>();
            var changesPropertyValues = new Dictionary<string, string>();

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
    }
}
