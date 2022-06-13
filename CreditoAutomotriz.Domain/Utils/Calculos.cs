using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Domain.Utils
{
    public static class Calculos
    {

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

    }
}
