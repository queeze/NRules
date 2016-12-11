namespace RuleBuilder.Core.Services
{
    using System;
    using System.Reflection;

    using Model;

    public class ContextTypeReflection
    {
        public ContextClass CreateContextClass(Type type, ContextClass parent = null)
        {
            ContextClass result = new ContextClass
            {
                Type = type,
                Name = type.Name,
                Parent = parent
            };

            var properties = type.GetRuntimeProperties();
            foreach (var propertyInfo in properties)
            {
                ContextClass existingType = FindType(propertyInfo.PropertyType, result.Parent);

                if (existingType != null)
                {
                    result.Properties.Add(new ContextClass
                    {
                        Name = propertyInfo.Name,
                        Parent = parent,
                        Type = propertyInfo.PropertyType,
                        Properties = existingType.Properties
                    });
                }
                else
                    result.Properties.Add(CreateContextClass(propertyInfo.PropertyType, result));
            }

            return result;
        }

        private ContextClass FindType(Type propertyType, ContextClass contextClass)
        {

            ContextClass result = null;
            if (contextClass != null)
            {
                if (contextClass.Type == propertyType)
                {
                    result = contextClass;
                }
                else
                {
                    foreach (var property in contextClass.Properties)
                    {
                        var t = FindType(propertyType, property);
                        if (t != null)
                        {
                            result = t;
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
