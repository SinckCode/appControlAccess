using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using appControlAccess1.Models;
using appControlAccess1.Services;
using Microsoft.Maui;

namespace appControlAccess1.Views
{
    public partial class UserManagementPage : ContentPage
    {
        private ObservableCollection<User> _usuarios = new();
        private readonly HttpClient _httpClient;
        private User _usuarioActualEditando;

        public UserManagementPage()
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
                var response = await _httpClient.GetAsync("/api/users");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<UserListResponse>(json);

                _usuarios.Clear();
                foreach (var u in resultado.data)
                    _usuarios.Add(u);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar los usuarios: {ex.Message}", "OK");
            }
        }

        private void OnAgregarUsuarioClicked(object sender, EventArgs e)
        {
            _usuarioActualEditando = null;
            MostrarModal();
        }

        private void OnEditarUsuarioClicked(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.BindingContext is User user)
            {
                _usuarioActualEditando = user;
                NombreEntry.Text = user.name;
                UidEntry.Text = user.rfid_uid;
                RolPicker.SelectedItem = user.role;
                MostrarModal();
            }
        }

        private async void OnEliminarUsuarioClicked(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.BindingContext is User user)
            {
                bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar al usuario {user.name}?", "Sí", "No");
                if (!confirm) return;

                try
                {
                    var response = await _httpClient.DeleteAsync($"/api/users/{user.id}");
                    response.EnsureSuccessStatusCode();
                    await DisplayAlert("Éxito", "Usuario eliminado", "OK");
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"No se pudo eliminar: {ex.Message}", "OK");
                }
            }
        }

        private async void OnGuardarUsuarioClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text?.Trim();
            var uid = UidEntry.Text?.Trim();
            var rol = RolPicker.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(rol))
            {
                await DisplayAlert("Error", "Completa todos los campos", "OK");
                return;
            }

            var nuevoUsuario = new { name = nombre, rfid_uid = uid, role = rol };
            var json = JsonConvert.SerializeObject(nuevoUsuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response;

                if (_usuarioActualEditando == null)
                {
                    response = await _httpClient.PostAsync("/api/users", content);
                }
                else
                {
                    response = await _httpClient.PutAsync($"/api/users/{_usuarioActualEditando.id}", content);
                }

                response.EnsureSuccessStatusCode();
                await DisplayAlert("Éxito", "Usuario guardado correctamente", "OK");
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
            NombreEntry.Text = UidEntry.Text = string.Empty;
            RolPicker.SelectedIndex = -1;
        }

        private void OnCancelarModalClicked(object sender, EventArgs e) => OcultarModal();
    }
}
