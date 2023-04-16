using WPFApplication.Core;

namespace WPFApplication.ViewModel
{
    public abstract class ChangeablePage : ObservableObject
    {
        public IPageHost? Successor { get; set; }

        public abstract void HandlePageChange(ChangeablePage page);
    }
}