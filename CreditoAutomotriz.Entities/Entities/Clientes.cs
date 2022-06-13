using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class Clientes
    {
        public Clientes()
        {
            ClientesPatiosAutos = new HashSet<ClientesPatiosAutos>();
            SolicitudCredito = new HashSet<SolicitudCredito>();
        }

        public string EstadoCivil { get; set; }
        public string IdentificacionConyuge { get; set; }
        public string NombresConyuge { get; set; }
        public int IdClientes { get; set; }
        public bool SujetoCredito { get; set; }

        public  Personas IdClientesNavigation { get; set; }
        public  ICollection<ClientesPatiosAutos> ClientesPatiosAutos { get; set; }
        public  ICollection<SolicitudCredito> SolicitudCredito { get; set; }
    }
}
