using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Entities.Dto.Vehiculos
{
    public class ConsultarVehiculosDto
    {

        public int IdMarca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }

    }
}
