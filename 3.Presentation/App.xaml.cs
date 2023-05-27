using _4.Entities;
using Microsoft.Extensions.Configuration;

namespace _3.Presentation
{
    public partial class App : Application
    {

        public static IConfiguration Configuration { get; set; }
        public static Usuario Usuario;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}