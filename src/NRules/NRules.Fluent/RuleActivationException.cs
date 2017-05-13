﻿using System;

namespace NRules.Fluent
{
    /// <summary>
    /// Represents errors that occur when instantiating rule classes.
    /// </summary>
#if NET45
    [System.Serializable]
#endif
    public class RuleActivationException : Exception
    {
        internal RuleActivationException(string message, Type ruleType, Exception innerException)
            : base(message, innerException)
        {
            RuleType = ruleType;
        }

#if NET45
        [System.Security.SecuritySafeCritical]
        protected RuleActivationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            RuleType = (Type)info.GetValue("RuleType", typeof(Type));
        }

        [System.Security.SecurityCritical]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            base.GetObjectData(info, context);
            info.AddValue("RuleType", RuleType, typeof(Type));
        }
#endif

        /// <summary>
        /// Rule .NET type that caused exception.
        /// </summary>
        public Type RuleType { get; private set; }

        public override string Message
        {
            get
            {
                string message = base.Message + Environment.NewLine + RuleType.FullName;
                return message;
            }
        }
    }
}
