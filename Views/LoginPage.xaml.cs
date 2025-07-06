using Microsoft.Maui.Controls;
using appControlAccess1.ViewModels;

namespace appControlAccess1.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
