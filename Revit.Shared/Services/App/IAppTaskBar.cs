using Hardcodet.Wpf.TaskbarNotification;

namespace Revit.Shared
{
    public interface IAppTaskBar
    {
        void Initialization();

        void Dispose();

        void ShowBalloonTip(string title, string message, BalloonIcon balloonIcon);
    }
}
