using appControlAccess1.Views;

namespace appControlAccess1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Iniciar directamente con LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
