using CreditoAutomotriz.Entities.MapFiles;
using TinyCsvParser.Mapping;

namespace CreditoAutomotriz.Repository.Mapping
{
    public class ClientesMap : CsvMapping<ClientesDtoFile>
    {
        public ClientesMap()
            : base()
        {
            MapProperty(0, x => x.Identificacion);
            MapProperty(1, x => x.Nombres);
            MapProperty(2, x => x.Apellidos);
            MapProperty(3, x => x.Direccion);
            MapProperty(4, x => x.Telefono);
            MapProperty(5, x => x.Edad);
            MapProperty(6, x => x.FechaNacimiento);
            MapProperty(7, x => x.EstadoCivil);
            MapProperty(8, x => x.IdentificacionConyuge);
            MapProperty(9, x => x.NombresConyuge);
            MapProperty(10, x => x.SujetoCredito);

        }
    }

}
