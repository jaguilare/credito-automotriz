using CreditoAutomotriz.Entities;
using CreditoAutomotriz.Entities.MapFiles;
using TinyCsvParser.Mapping;

namespace CreditoAutomotriz.Repository.Mapping
{
    public class EjecutivosMap : CsvMapping<EjecutivosDtoFile>
    {
        public EjecutivosMap()
            : base()
        {
            MapProperty(0, x => x.Identificacion);
            MapProperty(1, x => x.Nombres);
            MapProperty(2, x => x.Apellidos);
            MapProperty(3, x => x.Direccion);
            MapProperty(4, x => x.Telefono);
            MapProperty(5, x => x.Celular);
            MapProperty(6, x => x.PatioLabora);
            MapProperty(7, x => x.FechaNacimiento);
            MapProperty(8, x => x.Edad);
        }
    }

}
