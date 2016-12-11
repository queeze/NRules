namespace RuleBuilder.Presentation.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Prism.Commands;
    using Prism.Mvvm;

    public class CollectionViewModel<T> : BindableBase
        where T : new()
    {
        private T _selectedItem;

        public CollectionViewModel()
        {
            AddItemCommand = new DelegateCommand(OnAddItem);
            EditItemCommand = new DelegateCommand(OnEditItem);
            DeleteItemCommand = new DelegateCommand(OnDeleteItem);
        }

        public ICollection<T> Items { get; } = new ObservableCollection<T>();

        public DelegateCommand AddItemCommand { get; }

        public DelegateCommand EditItemCommand { get; }

        public DelegateCommand DeleteItemCommand { get; }

        public T SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        protected virtual void OnAddItem()
        {
            Items.Add(new T());
        }

        protected virtual void OnEditItem()
        {
        }

        protected virtual void OnDeleteItem()
        {
            Items.Remove(SelectedItem);
        }

    }
}
