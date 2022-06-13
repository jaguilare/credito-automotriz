using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Infrastructure.AppContextDB;
using CreditoAutomotriz.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository.Implementation
{
    public class UnitWork : IUnitWork
    {
        private readonly CreditoAutomotrizContext _dbContext;

        public UnitWork(CreditoAutomotrizContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IGenericRepository<Personas> _personas;
        public IGenericRepository<Personas> Personas
        {
            get
            {
                return _personas == null
                    ? _personas = new GenericRepository<Personas>(_dbContext)
                    : _personas;
            }
        }


        private IGenericRepository<Clientes> _clientes;
        public IGenericRepository<Clientes> Clientes
        {
            get
            {
                return _clientes == null
                    ? _clientes = new GenericRepository<Clientes>(_dbContext)
                    : _clientes;
            }
        }

        private IGenericRepository<Ejecutivos> _ejecutivos;
        public IGenericRepository<Ejecutivos> Ejecutivos
        {
            get
            {
                return _ejecutivos == null
                    ? _ejecutivos = new GenericRepository<Ejecutivos>(_dbContext)
                    : _ejecutivos;
            }
        }

        private IGenericRepository<PatiosAutos> _patiosAutos;
        public IGenericRepository<PatiosAutos> PatiosAutos
        {
            get
            {
                return _patiosAutos == null
                    ? _patiosAutos = new GenericRepository<PatiosAutos>(_dbContext)
                    : _patiosAutos;
            }
        }

        private IGenericRepository<MarcasVehiculos> _marcasVehiculo;
        public IGenericRepository<MarcasVehiculos> MarcasVehiculos
        {
            get
            {
                return _marcasVehiculo == null
                    ? _marcasVehiculo = new GenericRepository<MarcasVehiculos>(_dbContext)
                    : _marcasVehiculo;
            }
        }

        private IGenericRepository<Vehiculos> _vehiculo;
        public IGenericRepository<Vehiculos> Vehiculos
        {
            get
            {
                return _vehiculo == null
                    ? _vehiculo = new GenericRepository<Vehiculos>(_dbContext)
                    : _vehiculo;
            }
        }

        private IGenericRepository<SolicitudCredito> _solicitudCredito;
        public IGenericRepository<SolicitudCredito> SolicitudCredito
        {
            get
            {
                return _solicitudCredito == null
                    ? _solicitudCredito = new GenericRepository<SolicitudCredito>(_dbContext)
                    : _solicitudCredito;
            }
        }

        private IGenericRepository<ClientesPatiosAutos> _clientesPatiosAutos;
        public IGenericRepository<ClientesPatiosAutos> ClientesPatiosAutos
        {
            get
            {
                return _clientesPatiosAutos == null
                    ? _clientesPatiosAutos = new GenericRepository<ClientesPatiosAutos>(_dbContext)
                    : _clientesPatiosAutos;
            }
        }

        public async Task<int> Guardar()
        {
            return await _dbContext.SaveChangesAsync();
        }


    }
}
