using CreditoAutomotriz.Entities.App;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IArchivosService
    {
        Task<Respuesta> CargarCsv();

    }
}
