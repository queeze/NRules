using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBuilder.Desktop.Domain
{
    public class Address
    {
        public Customer Owner { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string AddressLine6 { get; set; }
        public string AddressLine7 { get; set; }
    }
}
