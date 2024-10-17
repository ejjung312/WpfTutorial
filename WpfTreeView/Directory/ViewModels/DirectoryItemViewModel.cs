using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfTreeView
{
    //public class DirectoryItemViewModel : BaseViewModel
    public partial class DirectoryItemViewModel : ObservableObject
    {
        #region Public Properties
        public DirectoryItemType Type { get; set; }

        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        public string FullPath { get; set; }

        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        private ObservableCollection<DirectoryItemViewModel> _children;
        public ObservableCollection<DirectoryItemViewModel> Children 
        { 
            get => _children;
            set => SetProperty(ref _children, value);
        }

        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                if (value == true)
                    Expand();
                else
                    this.ClearChildren();
            }
        }
        #endregion

        #region Constructor
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            this.FullPath = fullPath;
            this.Type = type;

            this.ClearChildren();
        }
        #endregion

        #region Helper Methods
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }
        #endregion
        [RelayCommand]
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
