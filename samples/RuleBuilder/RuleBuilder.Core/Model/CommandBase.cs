using System;

namespace RuleBuilder.Core.Model
{
    public abstract class CommandBase
    {
        protected CommandBase(int commandType)
        {
            CommandType = commandType;
        }

        internal void CheckVersion(int version)
        {
            if (ExpectedVersion != version)
            {
                throw new InvalidOperationException("Expected version mismatch");
            }
        }

        internal int ExpectedVersion { get; set; }

        internal int CommandType { get; }
    }
}