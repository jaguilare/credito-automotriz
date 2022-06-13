using CreditoAutomotriz.Entities.MapFiles;
using TinyCsvParser.Mapping;

namespace CreditoAutomotriz.Repository.Mapping
{
    public class MarcasMap : CsvMapping<MarcasVehiculosDtoFile>
    {

        public MarcasMap() : base()
        {
            MapProperty(0, x => x.Marca);
        }
    }
}
