using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class SolicitudCredito
    {
        public int IdSolicitudCredito { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public int IdClientes { get; set; }
        public int IdPatiosAustos { get; set; }
        public int IdVehiculos { get; set; }
        public short MesesPlazo { get; set; }
        public decimal Cuotas { get; set; }
        public decimal Entrada { get; set; }
        public string Observacion { get; set; }
        public short Estado { get; set; }
        public int IdEjecutivos { get; set; }

        public  Clientes IdClientesNavigation { get; set; }
        public  Ejecutivos IdEjecutivosNavigation { get; set; }
        public  PatiosAutos IdPatiosAustosNavigation { get; set; }
        public  Vehiculos IdVehiculosNavigation { get; set; }
    }
}
