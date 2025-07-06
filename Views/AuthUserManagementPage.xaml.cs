using appControlAccess1.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using appControlAccess1.Services;


namespace appControlAccess1.Views
{
    public partial class AuthUserManagementPage : ContentPage
    {
        private ObservableCollection<AuthUser> _usuarios = new();
        private readonly HttpClient _httpClient;
        private AuthUser _usuarioActualEditando;

        public AuthUserManagementPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(ApiConfig.BaseUrl) };
            UsuariosCollection.ItemsSource = _usuarios;
            CargarUsuarios();
        }

        private async void CargarUsuarios()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/auth-users");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var wrapper = JsonConvert.DeserializeObject<ApiResponse<List<AuthUser>>>(json);
                var usuarios = wrapper.data;

                _usuarios.Clear();
                foreach (var u in usuarios)
                    _usuarios.Add(u);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar los guardias: {ex.Message}", "OK");
            }
        }


        private void OnAgregarGuardiaClicked(object sender, EventArgs e)
        {
            _usuarioActualEditando = null;
            MostrarModal();
        }

        private void OnEditarGuardiaClicked(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.BindingContext is AuthUser user)
            {
                _usuarioActualEditando = user;
                NombreEntry.Text = user.name;
                CorreoEntry.Text = user.email;
                PasswordEntry.Text = user.password;
                RolPicker.SelectedItem = user.rol;
                MostrarModal();
            }
        }

        private async void OnEliminarGuardiaClicked(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.BindingContext is AuthUser user)
            {
                bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar al guardia {user.name}?", "Sí", "No");
                if (!confirm) return;

                try
                {
                    var response = await _httpClient.DeleteAsync($"/api/auth-users/{user.id}");
                    response.EnsureSuccessStatusCode();
                    await DisplayAlert("Éxito", "Guardia eliminado", "OK");
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo eliminar: {ex.Message}", "OK");
                }
            }
        }

        private async void OnGuardarGuardiaClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text?.Trim();
            var correo = CorreoEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();
            var rol = RolPicker.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(rol))
            {
                await DisplayAlert("Error", "Completa todos los campos", "OK");
                return;
            }

            var nuevoUsuario = new { name = nombre, email = correo, password = password, rol = rol };
            var json = JsonConvert.SerializeObject(nuevoUsuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response;

                if (_usuarioActualEditando == null)
                    response = await _httpClient.PostAsync("/api/auth-users", content);
                else
                    response = await _httpClient.PutAsync($"/api/auth-users/{_usuarioActualEditando.id}", content);

                response.EnsureSuccessStatusCode();
                await DisplayAlert("Éxito", "Guardia guardado correctamente", "OK");
                OcultarModal();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar: {ex.Message}", "OK");
            }
        }

        private void MostrarModal() => ModalLayout.IsVisible = true;

        private void OcultarModal()
        {
            ModalLayout.IsVisible = false;
            NombreEntry.Text = CorreoEntry.Text = PasswordEntry.Text = string.Empty;
            RolPicker.SelectedIndex = -1;
        }

        private void OnCancelarModalClicked(object sender, EventArgs e) => OcultarModal();
    }
}
