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
        bool _isEnable;
        public virtual bool IsEnabled
        {
            get => _isEnable;
            set
            {
                _isEnable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }
        public virtual int ParentId { get; set; }
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
        double rowHeight;
        public virtual double RowHeight
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
