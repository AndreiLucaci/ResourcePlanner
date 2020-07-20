using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel;
using System.Linq;

namespace TestScheduler.ViewModels
{
    [POCOViewModel]
    public class UserViewModel : INotifyPropertyChanged
    {
        public virtual string Parent { get; } = "department";
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        string color;
        public virtual string Color
        {
            get => color;
            set
            {
                color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            }
        }
        public virtual string Department { get; set; }
        int rowHeight;
        public virtual int RowHeight
        {
            get => rowHeight; set
            {
                rowHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RowHeight)));
            }
        }

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
