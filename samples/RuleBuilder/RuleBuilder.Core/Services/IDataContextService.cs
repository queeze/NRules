using System;
using RuleBuilder.Core.Model;

namespace RuleBuilder.Core.Services
{
    public interface IDataContextService
    {
        DataContext CreateDataContext(Type[] types);
    }
}