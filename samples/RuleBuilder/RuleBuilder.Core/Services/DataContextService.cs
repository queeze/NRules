namespace RuleBuilder.Core.Services
{
    using System;

    using Model;

    public class DataContextService
    {
        readonly ContextTypeReflection _typeReflection = new ContextTypeReflection();

        public DataContext CreateDataContext(Type[] types)
        {
            DataContext result = new DataContext();

            foreach (var type in types)
            {
                result.ContextTypes.Add(_typeReflection.CreateContextClass(type));    
            }

            return result;
        }
    }
}
