namespace RuleBuilder.Core.Services
{
    using System;

    using Storage;

    public class PoliciesService
    {
        private readonly IPoliciesRepository _policiesRepository;

        public PoliciesService(IPoliciesRepository policiesRepository)
        {
            if (policiesRepository == null)
                throw new ArgumentNullException(nameof(policiesRepository));

            _policiesRepository = policiesRepository;
        }


    }
}
