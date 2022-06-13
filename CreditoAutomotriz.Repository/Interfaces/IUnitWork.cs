using CreditoAutomotriz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository.Interfaces
{
    public interface IUnitWork
    {

        IGenericRepository<Personas> Personas { get; }
        IGenericRepository<Clientes> Clientes { get; }
        IGenericRepository<Ejecutivos> Ejecutivos { get; }
        IGenericRepository<PatiosAutos> PatiosAutos { get; }
        IGenericRepository<MarcasVehiculos> MarcasVehiculos { get; }
        IGenericRepository<Vehiculos> Vehiculos { get; }
        IGenericRepository<SolicitudCredito> SolicitudCredito { get; }
        IGenericRepository<ClientesPatiosAutos> ClientesPatiosAutos { get; }

        Task<int> Guardar();


    }
}
