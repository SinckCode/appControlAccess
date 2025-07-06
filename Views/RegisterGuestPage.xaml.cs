using appControlAccess1.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace appControlAccess1.Views
{
    public partial class RegisterGuestPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public RegisterGuestPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnRegistrarClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text?.Trim();
            var empresa = EmpresaEntry.Text?.Trim();
            var motivo = MotivoEntry.Text?.Trim();
            var uid = UidEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(empresa) ||
                string.IsNullOrWhiteSpace(motivo) || string.IsNullOrWhiteSpace(uid))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            var invitado = new
            {
                nombre = nombre,
                empresa = empresa,
                motivo = motivo,
                rfid_uid = uid
            };

            var json = JsonConvert.SerializeObject(invitado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var url = $"{ApiConfig.BaseUrl}/api/access/invite";
                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Invitado registrado correctamente", "OK");
                    NombreEntry.Text = EmpresaEntry.Text = MotivoEntry.Text = UidEntry.Text = "";
                }
                else
                {
                    await DisplayAlert("Error", $"Error al registrar: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Excepción: {ex.Message}", "OK");
            }
        }
    }
}
