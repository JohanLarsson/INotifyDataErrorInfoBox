using JetBrains.Annotations;

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
            this.errors = new PropertyErrors(this, this.OnErrorsChanged);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

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

        private void Search()
        {
            if (string.IsNullOrEmpty(this.searchText))
            {
                this.errors.Add(nameof(this.SearchText), "Search text cannot be empty");
                return;
            }

            MessageBox.Show("searching");
        }

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            this.ErrorsChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}