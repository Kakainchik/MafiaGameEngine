using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFApplication.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region ImplementInterface

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}