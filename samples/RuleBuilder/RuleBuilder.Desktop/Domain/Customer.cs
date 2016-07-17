namespace RuleBuilder.Desktop.Domain
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Address = new Address() { Owner = this };
        }

        public string Name { get; set; }

        public Address Address { get; set; }
    }

}
