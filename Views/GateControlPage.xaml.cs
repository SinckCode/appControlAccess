using appControlAccess1.Services;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace appControlAccess1.Views
{
    public partial class GateControlPage : ContentPage
    {
        private readonly GateService _gateService;

        public GateControlPage()
        {
            InitializeComponent();
            _gateService = new GateService();
        }

        // PLUMA 1
        private async void OnOpen1Clicked(object sender, EventArgs e) =>
            await ControlGateAsync(1, "open");

        private async void OnClose1Clicked(object sender, EventArgs e) =>
            await ControlGateAsync(1, "close");

        private async void OnCheckStatus1Clicked(object sender, EventArgs e) =>
            await UpdateStatusAsync(1);

        // PLUMA 2
        private async void OnOpen2Clicked(object sender, EventArgs e) =>
            await ControlGateAsync(2, "open");

        private async void OnClose2Clicked(object sender, EventArgs e) =>
            await ControlGateAsync(2, "close");

        private async void OnCheckStatus2Clicked(object sender, EventArgs e) =>
            await UpdateStatusAsync(2);

        private async Task ControlGateAsync(int gate, string action)
        {
            bool success = action == "open"
                ? await _gateService.OpenGateAsync(gate)
                : await _gateService.CloseGateAsync(gate);

            await DisplayAlert($"Pluma {gate}",
                success ? $"Pluma {gate} {action}ada" : $"Error al {action} Pluma {gate}",
                "OK");

            await UpdateStatusAsync(gate);
        }

        private async Task UpdateStatusAsync(int gate)
        {
            string json = await _gateService.GetGateStatusAsync(gate);

            if (!string.IsNullOrEmpty(json) && !json.StartsWith("ERROR"))
            {
                var resultado = JsonConvert.DeserializeObject<GateStatusResponse>(json);

                string estado = resultado?.state == "open" ? "Abierta" :
                                resultado?.state == "closed" ? "Cerrada" : "Desconocida";

                if (gate == 1)
                    StatusLabel1.Text = $"Estado: {estado}";
                else
                    StatusLabel2.Text = $"Estado: {estado}";
            }
            else
            {
                if (gate == 1)
                    StatusLabel1.Text = "Estado: Error al obtener estado";
                else
                    StatusLabel2.Text = "Estado: Error al obtener estado";
            }
        }
    }


}
