using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Dto.Vehiculos;
using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Entities.Values;
using CreditoAutomotriz.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Implementations
{
    public class VehiculosService : IVehiculosService
    {

        private readonly IUnitWork _unitWork;

        private string _mensajeRespuesta;
        private object _resultadoRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        public VehiculosService(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _mensajeRespuesta = MensajesRespuesta.OK;
            _resultadoRespuesta = null;
            _codigoRespuesta = ECodigoRespuesta.OK;
        }


        public async Task<Respuesta> Actualizar(Vehiculos cambioVehiculo)
        {
            try
            {
                Func<Vehiculos, bool> predicado =
                    e => e.IdVehiculos.Equals(cambioVehiculo.IdVehiculos);

                Vehiculos vehiculo = await _unitWork.Vehiculos.ConsultarFiltro(predicado);

                if (vehiculo != null)
                {
                    vehiculo.Placa = cambioVehiculo.Placa;
                    vehiculo.Avaluo = cambioVehiculo.Avaluo;

                    _unitWork.Vehiculos.Actualizar(vehiculo);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_VEHICULO_NO_ENCONTRADO);
                }
            }
            catch (Exception exc)
            {
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _resultadoRespuesta = null;
                _mensajeRespuesta = exc.Message;
            }

            return new Respuesta()
            {
                Codigo = _codigoRespuesta,
                Mensaje = _mensajeRespuesta,
                Resultado = _resultadoRespuesta
            };
        }

        public async Task<Respuesta> Agregar(Vehiculos nuevoVehiculo)
        {
            try
            {
                Func<Vehiculos, bool> predicado =
                    e => e.Placa.Equals(nuevoVehiculo.Placa)
                    || e.NroChasis.Equals(nuevoVehiculo.NroChasis);

                Vehiculos vehiculo = await _unitWork.Vehiculos.ConsultarFiltro(predicado);

                if (vehiculo == null)
                {
                    nuevoVehiculo = await _unitWork.Vehiculos.Agregar(nuevoVehiculo);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_VEHICULO_YA_EXISTE);
                }
                _resultadoRespuesta = nuevoVehiculo;
            }
            catch (Exception exc)
            {
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _resultadoRespuesta = null;
                _mensajeRespuesta = exc.Message;
            }

            return new Respuesta()
            {
                Codigo = _codigoRespuesta,
                Mensaje = _mensajeRespuesta,
                Resultado = _resultadoRespuesta
            };
        }

        public async Task<Respuesta> Consultar(ConsultarVehiculosDto dto)
        {
            //Considerar los campos marca o modelo o año para la consulta de vehiculos
            try
            {
                Func<Vehiculos, bool> predicado =
                    e => e.IdMarcaVehiculo == dto.IdMarca
                    || e.Modelo.Equals(dto.Modelo)
                    || e.Anio.Equals(dto.Anio);

                Vehiculos vehiculo = await _unitWork.Vehiculos.ConsultarFiltro(predicado);

                if (vehiculo == null)
                {
                    throw new Exception(MensajesRespuesta.WARNING_VEHICULO_NO_ENCONTRADO);
                }
                _resultadoRespuesta = vehiculo;
            }
            catch (Exception exc)
            {
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _resultadoRespuesta = null;
                _mensajeRespuesta = exc.Message;
            }

            return new Respuesta()
            {
                Codigo = _codigoRespuesta,
                Mensaje = _mensajeRespuesta,
                Resultado = _resultadoRespuesta
            };
        }

        public async Task<Respuesta> Eliminar(string placa)
        {
            try
            {
                Func<Vehiculos, bool> predicado =
                    e => e.Placa.Equals(placa);

                Vehiculos vehiculo = await _unitWork.Vehiculos.ConsultarFiltro(predicado);

                if (vehiculo == null)
                {
                    throw new Exception(MensajesRespuesta.WARNING_VEHICULO_NO_ENCONTRADO);
                }

                _unitWork.Vehiculos.Eliminar(vehiculo);
                await _unitWork.Guardar();
            }
            catch (DbUpdateException exc)
            {
                if (exc.HResult == (int)ECodigosDB.REGISTROS_DEPENDIENTES)
                {
                    _codigoRespuesta = ECodigoRespuesta.ERROR;
                    _resultadoRespuesta = null;
                    _mensajeRespuesta = MensajesRespuesta.WARNING_REGISTRO_DEPENDIENTE;
                }
            }
            catch (Exception exc)
            {
                _codigoRespuesta = ECodigoRespuesta.ERROR;
                _resultadoRespuesta = null;
                _mensajeRespuesta = exc.Message;
            }

            return new Respuesta()
            {
                Codigo = _codigoRespuesta,
                Mensaje = _mensajeRespuesta,
                Resultado = _resultadoRespuesta
            };
        }
    }
}
