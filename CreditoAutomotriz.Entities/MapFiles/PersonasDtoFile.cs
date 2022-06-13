using System;

namespace CreditoAutomotriz.Entities.MapFiles
{
    public class PersonasDtoFile
    {
        public int IdPersonas { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte Edad { get; set; }
    }
}
