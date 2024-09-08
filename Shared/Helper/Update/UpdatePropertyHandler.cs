public static class UpdatePropertyHandler
{
    public static void UpdateProperties<TEntity, TDto>(
        TEntity entity,
        TDto dto)
    {
        var entityProperties = typeof(TEntity).GetProperties();
        var dtoProperties = typeof(TDto).GetProperties();

        foreach (var dtoProp in dtoProperties)
        {
            var dtoValue = dtoProp.GetValue(dto);

            if (dtoValue != null &&
                (dtoValue is string strValue ? !string.IsNullOrWhiteSpace(strValue) : true))
            {
                var entityProp = entityProperties.FirstOrDefault(p => p.Name == dtoProp.Name);
                if (entityProp != null && entityProp.CanWrite)
                {
                    var entityPropValue = entityProp.GetValue(entity);
                    var entityPropType = entityProp.PropertyType;

                    bool shouldUpdate = false;

                    if (entityPropType == typeof(string))
                    {
                        var stringValue = dtoValue as string;
                        var existingStringValue = entityPropValue as string;
                        if (!string.IsNullOrWhiteSpace(stringValue) && !string.Equals(existingStringValue, stringValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(int) || entityPropType == typeof(int?))
                    {
                        if (dtoValue is int intValue && !Equals(entityPropValue, intValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(long) || entityPropType == typeof(long?))
                    {
                        if (dtoValue is long longValue && !Equals(entityPropValue, longValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(short) || entityPropType == typeof(short?))
                    {
                        if (dtoValue is short shortValue && !Equals(entityPropValue, shortValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(float) || entityPropType == typeof(float?))
                    {
                        if (dtoValue is float floatValue && !Equals(entityPropValue, floatValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(double) || entityPropType == typeof(double?))
                    {
                        if (dtoValue is double doubleValue && !Equals(entityPropValue, doubleValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(decimal) || entityPropType == typeof(decimal?))
                    {
                        if (dtoValue is decimal decimalValue && !Equals(entityPropValue, decimalValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(bool))
                    {
                        if (dtoValue is bool boolValue && !Equals(entityPropValue, boolValue))
                        {
                            shouldUpdate = true;
                        }
                    }
                    else if (entityPropType == typeof(DateTime) || entityPropType == typeof(DateTime?))
                    {
                        if (dtoValue is DateTime dateTimeValue && !Equals(entityPropValue, dateTimeValue))
                        {
                            shouldUpdate = true;
                        }
                    }

                    if (shouldUpdate)
                    {
                        entityProp.SetValue(entity, dtoValue);
                    }
                }
            }
        }
    }
}
