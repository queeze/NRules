namespace RuleBuilder.Desktop.ViewModels
{
    using System;

    using Prism.Mvvm;

    public class ContextObjectViewModel : BindableBase
    {
        private Type _contextObectType;

        public void ContextObjectType(Type contextObectType)
        {
            if (contextObectType == null) throw new ArgumentNullException(nameof(contextObectType));

            _contextObectType = contextObectType;
        }
    }
}
