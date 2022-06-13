using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class Vehiculos
    {
        public Vehiculos()
        {
            SolicitudCredito = new HashSet<SolicitudCredito>();
        }

        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NroChasis { get; set; }
        public short IdMarcaVehiculo { get; set; }
        public string Tipo { get; set; }
        public string Cilindraje { get; set; }
        public decimal Avaluo { get; set; }
        public short Anio { get; set; }
        public int IdVehiculos { get; set; }

        public MarcasVehiculos IdMarcaVehiculoNavigation { get; set; }
        public ICollection<SolicitudCredito> SolicitudCredito { get; set; }
    }
}
