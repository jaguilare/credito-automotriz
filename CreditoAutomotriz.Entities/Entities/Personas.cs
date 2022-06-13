using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class Personas
    {
        public int IdPersonas { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public byte Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public  Clientes Clientes { get; set; }
        public  Ejecutivos Ejecutivos { get; set; }
    }
}
