﻿using System.Collections.Generic;
using System.Linq;
using NRules.Dsl;
using NRules.IntegrationTests.TestAssets;

namespace NRules.IntegrationTests.TestRules
{
    public class TwoFactOneCollectionRule : BaseRule
    {
        public int FactCount { get; set; }

        public override void Define(IDefinition definition)
        {
            FactType1 fact1 = null;
            IEnumerable<FactType2> collection2 = null;

            definition.When()
                .If<FactType1>(() => fact1, f => f.TestProperty == "Valid Value")
                .Collect<FactType2>(() => collection2, f => f.TestProperty.StartsWith("Valid"), f => f.JoinReference == fact1);
            definition.Then()
                .Do(ctx => Notifier.RuleActivated())
                .Do(ctx => SetCount(collection2.Count()));
        }

        private void SetCount(int count)
        {
            FactCount = count;
        }
    }
}