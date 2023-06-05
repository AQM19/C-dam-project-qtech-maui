using _2.BusinessLogic;
using _4.Entities;

namespace _3.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            
            NotificationPollingComponent.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(NotificationPollingComponent.PendingNotifications))
                {
                    Notifications.Icon = NotificationPollingComponent.PendingNotifications ? "campana_on.png" : "campana_off.png";
                }
            };
        }

        
    }
}