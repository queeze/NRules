namespace RuleBuilder.Desktop
{
    using System.Windows;

    using Core.Services;
    using Core.Model;
    using Domain;
    using Presentation.Services;
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContextService dcs = new DataContextService();

            var dataContext = dcs.CreateDataContext(
                new[]
                {
                    typeof (Customer),
                    typeof (Order)
                });

            DataContext = new MainWindowViewModel();

            var basePolicy = CreateTestPolicy(dataContext);

            var viewModelServices = new ViewModelServices();
            var policiesViewModel = viewModelServices.CreatePoliciesViewModel(new[] {basePolicy});

            PoliciesTreeView.DataContext = policiesViewModel;
        }

        private static Policy CreateTestPolicy(DataContext dataContext)
        {
            Policy basePolicy = new Policy("Base Policy");
            basePolicy.DataContext = dataContext;
            basePolicy.AddFact(new Fact { Name = "Fact01" });
            basePolicy.AddFact(new Fact { Name = "Fact02" });

            var preDip = new Policy("PDIP");
            preDip.AddRule(new Rule { Name = "PDIP01" });
            preDip.AddRule(new Rule { Name = "PDIP02" });
            preDip.AddFact(new Fact { Name = "Fact01" });
            preDip.AddFact(new Fact { Name = "Fact02" });

            basePolicy.AddPolicy(preDip);

            var dip = new Policy("DIP");
            dip.AddRule(new Rule { Name = "DIP01" });
            dip.AddRule(new Rule { Name = "DIP02" });

            dip.AddFact(new Fact { Name = "Fact01" });
            dip.AddFact(new Fact { Name = "Fact02" });

            basePolicy.AddPolicy(dip);

            return basePolicy;
        }
    }
}
