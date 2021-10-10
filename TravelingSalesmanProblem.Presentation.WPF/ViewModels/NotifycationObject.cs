using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TravelingSalesmanProblem.Presentation.WPF.ViewModels
{
    internal abstract class NotifycationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private protected void SetProperty<T>(ref T target, T value, [CallerMemberName] string caller = "")
        {
            target = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}