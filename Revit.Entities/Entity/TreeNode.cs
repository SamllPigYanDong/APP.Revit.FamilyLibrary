using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Revit.Entity.Entity
{
    public class TreeNode<T> : BindableBase where T : class
    {
        public T Value { get; set; }


       private ObservableCollection<TreeNode<T>> _childs  = new ObservableCollection<TreeNode<T>>();
        public ObservableCollection<TreeNode<T>> Childs
        {
            get { return _childs; }
            set { SetProperty(ref _childs, value); }
        } 

        public string Name { get; set; }


        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}