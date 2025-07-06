using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using appControlAccess1.Models;
using appControlAccess1.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace appControlAccess1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private bool _isLoading;

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingresa correo y contraseña.", "OK");
                return;
            }

            IsLoading = true;

            var response = await new AuthService().LoginAsync(new LoginRequest
            {
                email = Email,
                password = Password
            });

            IsLoading = false;

            if (response != null && response.success)
            {
                Preferences.Set("username", Email);
                await Application.Current.MainPage.Navigation.PushAsync(new Views.DashboardPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Credenciales inválidas. Intenta nuevamente.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
