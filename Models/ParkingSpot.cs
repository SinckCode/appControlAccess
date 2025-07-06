using System;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace appControlAccess1.Models
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public int Is_Occupied { get; set; }
        public int Is_Reserved { get; set; }
        public int Is_Heat_Detected { get; set; }
        public string Updated_At { get; set; }

        // Propiedad calculada para texto del estado
        public string EstadoTexto
        {
            get
            {
                if (Is_Reserved == 1)
                    return "Reservado";
                else if (Is_Occupied == 1)
                    return "Ocupado";
                else
                    return "Libre";
            }
        }

        // Propiedad calculada para color del estado
        public Color EstadoColor
        {
            get
            {
                if (Is_Reserved == 1)
                    return Colors.Orange;
                else if (Is_Occupied == 1)
                    return Colors.Red;
                else
                    return Colors.Green;
            }
        }
    }
}
