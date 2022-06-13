using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Implementations
{
    public class ClientesPatiosAutosService : IClientesPatiosAutosService
    {

        private readonly IUnitWork _unitWork;

        private string _mensajeRespuesta;
        private object _resultadoRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        public ClientesPatiosAutosService(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _mensajeRespuesta = MensajesRespuesta.OK;
            _resultadoRespuesta = null;
            _codigoRespuesta = ECodigoRespuesta.OK;
        }

        public async Task<Respuesta> Actualizar(ClientesPatiosAutos cambioClientePatio)
        {
            try
            {
                Func<ClientesPatiosAutos, bool> predicado =
                    e => e.IdClientesPatiosAutos.Equals(cambioClientePatio.IdClientesPatiosAutos);

                ClientesPatiosAutos clientePatio = await _unitWork.ClientesPatiosAutos.ConsultarFiltro(predicado);

                if (clientePatio != null)
                {
                    clientePatio.IdPatiosAutos = cambioClientePatio.IdPatiosAutos;
                    clientePatio.FechaAsignacion = DateTime.Now;

                    _unitWork.ClientesPatiosAutos.Actualizar(clientePatio);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_CLIENTE_PATIO_AUTO_NO_ENCONTRADO);
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

        public async Task<Respuesta> Agregar(ClientesPatiosAutos nuevoClientePatio)
        {
            try
            {
                nuevoClientePatio.FechaAsignacion = DateTime.Now;

                Func<ClientesPatiosAutos, bool> predicado =
                    e => e.IdClientes == nuevoClientePatio.IdClientes
                    && e.IdPatiosAutos == nuevoClientePatio.IdPatiosAutos;

                ClientesPatiosAutos clientePatio = await _unitWork.ClientesPatiosAutos.ConsultarFiltro(predicado);

                if (clientePatio == null)
                {
                    nuevoClientePatio = await _unitWork.ClientesPatiosAutos.Agregar(nuevoClientePatio);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_VEHICULO_YA_EXISTE);
                }
                _resultadoRespuesta = nuevoClientePatio;
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

        public async Task<Respuesta> Consultar(string identificacionCliente)
        {
            try
            {
                ClientesPatiosAutos clientePatioAuto = null;

                Func<Personas, bool> predicadoPersona =
                    e => e.Identificacion.Equals(identificacionCliente);

                Personas persona = await _unitWork.Personas.ConsultarFiltro(predicadoPersona);

                if (persona != null)
                {
                    Func<ClientesPatiosAutos, bool> predicadoClientePatioAuto =
                        e => e.IdClientes == persona.IdPersonas;

                    clientePatioAuto =
                        await _unitWork.ClientesPatiosAutos.ConsultarFiltro(predicadoClientePatioAuto);

                    if (clientePatioAuto == null)
                    {
                        throw new Exception(MensajesRespuesta.WARNING_CLIENTE_PATIO_AUTO_NO_ENCONTRADO);
                    }
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_CLIENTES_NO_ENCONTRADO);
                }

                _resultadoRespuesta = clientePatioAuto;
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

        public async Task<Respuesta> Eliminar(string identificacionCliente)
        {
            try
            {
                ClientesPatiosAutos clientePatioAuto = null;

                Func<Personas, bool> predicadoPersona =
                    e => e.Identificacion.Equals(identificacionCliente);

                Personas persona = await _unitWork.Personas.ConsultarFiltro(predicadoPersona);

                if (persona != null)
                {
                    Func<ClientesPatiosAutos, bool> predicadoClientePatioAuto =
                        e => e.IdClientes == persona.IdPersonas;

                    clientePatioAuto =
                        await _unitWork.ClientesPatiosAutos.ConsultarFiltro(predicadoClientePatioAuto);

                    if (clientePatioAuto == null)
                    {
                        throw new Exception(MensajesRespuesta.WARNING_CLIENTE_PATIO_AUTO_NO_ENCONTRADO);
                    }

                    _unitWork.ClientesPatiosAutos.Eliminar(clientePatioAuto);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_CLIENTES_NO_ENCONTRADO);
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
