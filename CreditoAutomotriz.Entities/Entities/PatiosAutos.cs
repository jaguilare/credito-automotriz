using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class PatiosAutos
    {
        public PatiosAutos()
        {
            ClientesPatiosAutos = new HashSet<ClientesPatiosAutos>();
            Ejecutivos = new HashSet<Ejecutivos>();
            SolicitudCredito = new HashSet<SolicitudCredito>();
        }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NroPuntoVenta { get; set; }
        public int IdPationsAutos { get; set; }

        public  ICollection<ClientesPatiosAutos> ClientesPatiosAutos { get; set; }
        public  ICollection<Ejecutivos> Ejecutivos { get; set; }
        public  ICollection<SolicitudCredito> SolicitudCredito { get; set; }
    }
}
