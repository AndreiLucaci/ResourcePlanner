using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class UserViewModel : INotifyPropertyChanged
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
        public virtual string Department { get; set; }

        private bool _isVisible = true;

        public virtual bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
