using System;
using System.Collections.Generic;
using System.Text;

namespace CreditoAutomotriz.Entities.App
{
    public class Respuesta
    {

        public ECodigoRespuesta Codigo { get; set; }
        public string Mensaje { get; set; }
        public object Resultado { get; set; }

    }
}
