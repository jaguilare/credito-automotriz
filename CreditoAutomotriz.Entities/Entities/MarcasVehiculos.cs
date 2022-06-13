using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Entities.Entities
{
    public partial class MarcasVehiculos
    {
        public MarcasVehiculos()
        {
            Vehiculos = new HashSet<Vehiculos>();
        }

        public short IdMarcasVehiculos { get; set; }
        public string Marca { get; set; }

        public  ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
