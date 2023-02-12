using WPFApplication.Model;

namespace WPFApplication.ViewModel
{
    public interface IScreenFactory
    {
        ScreenState Create(RoleVisual role, bool isAlive);
    }
}