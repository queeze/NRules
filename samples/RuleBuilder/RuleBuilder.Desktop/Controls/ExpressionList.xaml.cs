using System.Windows.Controls;
using System.Windows.Input;

namespace RuleBuilder.Desktop.Controls
{
    /// <summary>
    /// Interaction logic for DynamicLinqUserControl.xaml
    /// </summary>
    public partial class ExpressionList : UserControl
    {
        public ExpressionList()
        {
            InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var vm = DataContext as ExpressionCollection;
            var param = e.Parameter as ExpressionListViewModel;
            if (vm != null && param != null)
            {
                vm.Remove(param);
            }
        }
    }
}
