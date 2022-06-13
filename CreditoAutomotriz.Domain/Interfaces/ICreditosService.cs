using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface ICreditosService
    {

        Task<Respuesta> Agregar(SolicitudCredito dto);

    }
}
