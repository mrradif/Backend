public static class AuditableEntityHandler
{
    public static void SetProperties<T>(T entity, string operationType) where T : class
    {
        var user = UserHelper.AppUser();

        if (user == null)
        {
            throw new InvalidOperationException("User is not logged in. Please login to perform this operation.");
        }

        if (operationType == "Add")
        {
            SetCreationProperties(entity, user.EmployeeId ?? user.Id.ToString(), DateTime.Now);
        }
        else if (operationType == "Update")
        {
            SetUpdateProperties(entity, user.EmployeeId ?? user.Id.ToString(), DateTime.Now);
        }
        else if (operationType == "Delete")
        {
            SetDeleteProperties(entity, user.EmployeeId ?? user.Id.ToString(), DateTime.Now);
        }
        else
        {
            throw new ArgumentException("Invalid operation type specified.");
        }
    }

    private static void SetCreationProperties<T>(T entity, string createdBy, DateTime createdDate) where T : class
    {
        var createdByProperty = typeof(T).GetProperty("CreatedBy");
        var createdDateProperty = typeof(T).GetProperty("CreatedDate");

        if (createdByProperty != null && createdByProperty.CanWrite)
        {
            createdByProperty.SetValue(entity, createdBy);
        }

        if (createdDateProperty != null && createdDateProperty.CanWrite)
        {
            createdDateProperty.SetValue(entity, createdDate);
        }
    }

    private static void SetUpdateProperties<T>(T entity, string updatedBy, DateTime updatedDate) where T : class
    {
        var updatedByProperty = typeof(T).GetProperty("UpdatedBy");
        var updatedDateProperty = typeof(T).GetProperty("UpdatedDate");

        if (updatedByProperty != null && updatedByProperty.CanWrite)
        {
            updatedByProperty.SetValue(entity, updatedBy);
        }

        if (updatedDateProperty != null && updatedDateProperty.CanWrite)
        {
            updatedDateProperty.SetValue(entity, updatedDate);
        }
    }

    private static void SetDeleteProperties<T>(T entity, string deletedBy, DateTime deletedDate) where T : class
    {
        var deletedByProperty = typeof(T).GetProperty("DeletedBy");
        var deletedDateProperty = typeof(T).GetProperty("DeletedDate");

        if (deletedByProperty != null && deletedByProperty.CanWrite)
        {
            deletedByProperty.SetValue(entity, deletedBy);
        }

        if (deletedDateProperty != null && deletedDateProperty.CanWrite)
        {
            deletedDateProperty.SetValue(entity, deletedDate);
        }
    }
}
