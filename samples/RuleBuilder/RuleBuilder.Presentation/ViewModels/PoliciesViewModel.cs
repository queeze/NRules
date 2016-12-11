namespace RuleBuilder.Presentation.ViewModels
{
    using Core.Model;

    public class PoliciesViewModel : CollectionViewModel<NodeViewModel>
    {
        protected override void OnAddItem()
        {

        }

        protected override void OnDeleteItem()
        {
            
        }

        protected override void OnEditItem()
        {
            if (SelectedItem != null)
            {
                
            }
        }
    }
}
