using RuleBuilder.Desktop.Controls;
using RuleBuilder.Desktop.Domain;
using System.ComponentModel;

namespace RuleBuilder.Desktop
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ExpressionCollection ExpressionList1 { get; set; }

        public MainWindowViewModel()
        {
            ExpressionList1 = new ExpressionCollection() { Type = typeof(Customer) };
            ExpressionList1.Add(new ExpressionListViewModel());
        }

    }
}
