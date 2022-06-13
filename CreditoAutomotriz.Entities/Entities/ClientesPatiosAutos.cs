using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class ClientesPatiosAutos
    {
        public int IdClientesPatiosAutos { get; set; }
        public int IdClientes { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int IdPatiosAutos { get; set; }

        public  Clientes IdClientesNavigation { get; set; }
        public  PatiosAutos IdPatiosAutosNavigation { get; set; }
    }
}
