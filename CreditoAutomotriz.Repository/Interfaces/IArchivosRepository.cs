using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace CreditoAutomotriz.Repository.Interfaces
{
    public interface IArchivosRepository<T>
    {
        List<T> CargarCsv<T>(string fileName, ICsvMapping<T> mapping);

    }
}
