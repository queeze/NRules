﻿using System;
using System.Diagnostics;
using System.Reflection;

namespace NRules.Rete
{
    [DebuggerDisplay("Type {FilterType.FullName,nq}")]
    internal class TypeNode : AlphaNode
    {
        public TypeNode(Type filterType)
        {
            FilterType = filterType;
        }

        public Type FilterType { get; private set; }

        public override bool IsSatisfiedBy(IExecutionContext context, Fact fact)
        {
            bool isMatchingType = FilterType.GetTypeInfo().IsAssignableFrom(fact.FactType.GetTypeInfo());
            return isMatchingType;
        }

        protected override void UnsatisfiedFactUpdate(IExecutionContext context, Fact fact)
        {
            //Do nothing, since fact type will never change
        }

        public override void Accept<TContext>(TContext context, ReteNodeVisitor<TContext> visitor)
        {
            visitor.VisitTypeNode(context, this);
        }
    }
}