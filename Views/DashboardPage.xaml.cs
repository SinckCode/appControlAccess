using appControlAccess1.Models;
using appControlAccess1.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace appControlAccess1.Views
{
    public partial class DashboardPage : ContentPage
    {
        private ObservableCollection<ParkingSpot> _parkingSpots;
        private ObservableCollection<AccessLog> _accessLogs;
        private bool _isRefreshing = false;
        private const int RefreshIntervalSeconds = 10;

        public DashboardPage()
        {
            InitializeComponent();

            // Inicialización de colecciones
            _parkingSpots = new ObservableCollection<ParkingSpot>();
            _accessLogs = new ObservableCollection<AccessLog>();

            // Asignación de datos a CollectionView
            ParkingStatusView.ItemsSource = _parkingSpots;
            LogsListView.ItemsSource = _accessLogs;

            // Comienza el refresco automático
            StartAutoRefresh();

            // Carga inicial
            _ = RefreshData();
        }

        private void StartAutoRefresh()
        {
            Device.StartTimer(TimeSpan.FromSeconds(RefreshIntervalSeconds), () =>
            {
                if (!_isRefreshing)
                {
                    Device.BeginInvokeOnMainThread(async () => await RefreshData());
                }

                return true; // Sigue ejecutando el timer
            });
        }

        private async Task RefreshData()
        {
            _isRefreshing = true;

            try
            {
                // Llamada a servicios
                var spots = await ParkingService.GetParkingStatusAsync();
                var logs = await AccessLogService.GetRecentLogsAsync();

                // Refresca estacionamiento
                _parkingSpots.Clear();
                foreach (var spot in spots)
                {
                    _parkingSpots.Add(spot);
                }

                // Refresca logs
                _accessLogs.Clear();
                foreach (var log in logs)
                {
                    // Validación mínima (puedes quitar si ya estás seguro)
                    if (!string.IsNullOrWhiteSpace(log.rfid_uid))
                    {
                        _accessLogs.Add(log);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al actualizar: {ex.Message}", "OK");
            }

            _isRefreshing = false;
        }

        private async void OnPlumaClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirmación", "¿Deseas activar la pluma manualmente?", "Sí", "Cancelar");
            if (confirm)
            {
                await DisplayAlert("Pluma", "Pluma activada manualmente (demo)", "OK");
                // Aquí iría la lógica real con tu API
            }
        }
        private async void OnIrARegistroInvitado(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterGuestPage());

        }

        private async void OnControlarPlumaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GateControlPage());
        }

        private async void OnGestionarUsuariosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserManagementPage());
        }

        private async void OnGestionarGuardiasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuthUserManagementPage());
        }





        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Cerrar sesión", "¿Estás seguro de que deseas cerrar sesión?", "Sí", "Cancelar");
            if (confirm)
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
}
