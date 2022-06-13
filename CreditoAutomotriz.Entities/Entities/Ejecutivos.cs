using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class Ejecutivos
    {
        public Ejecutivos()
        {
            SolicitudCredito = new HashSet<SolicitudCredito>();
        }

        public int IdEjecutivos { get; set; }
        public string Celular { get; set; }
        public int IdPatiosAutos { get; set; }

        public  Personas IdEjecutivosNavigation { get; set; }
        public  PatiosAutos IdPatiosAutosNavigation { get; set; }
        public  ICollection<SolicitudCredito> SolicitudCredito { get; set; }
    }
}
