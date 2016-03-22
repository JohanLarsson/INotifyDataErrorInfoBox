namespace ValidationBox
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string searchText;

        private readonly PropertyErrors errors;

        public ViewModel()
        {
            this.SearchCommand = new RelayCommand(this.Search);
            this.errors = new PropertyErrors(this);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add
            {
                this.errors.ErrorsChanged += value;
            }
            remove
            {
                this.errors.ErrorsChanged -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string SearchText
        {
            get { return this.searchText; }
            set
            {
                if (value == this.searchText)
                {
                    return;
                }

                this.searchText = value;
                this.errors.Clear(nameof(this.SearchText));
                this.OnPropertyChanged();
            }
        }

        public bool HasErrors => this.errors.HasErrors;

        public ICommand SearchCommand { get; }

        public IEnumerable GetErrors(string propertyName) => this.errors.GetErrors(propertyName);

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.searchText))
            {
                this.errors.Add(nameof(this.SearchText), "Search text cannot be empty");
                return;
            }

            MessageBox.Show("searching");
        }
    }
}