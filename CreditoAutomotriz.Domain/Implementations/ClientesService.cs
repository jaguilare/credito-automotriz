using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Repository.Interfaces;
using System;
using System.Threading.Tasks;
using CreditoAutomotriz.Domain.Utils;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using CreditoAutomotriz.Entities.Values;

namespace CreditoAutomotriz.Domain.Implementations
{
    public class ClientesService : IClientesService
    {
        private readonly IUnitWork _unitWork;

        private string _mensajeRespuesta;
        private object _resultadoRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        public ClientesService(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _mensajeRespuesta = MensajesRespuesta.OK;
            _resultadoRespuesta = null;
            _codigoRespuesta = ECodigoRespuesta.OK;
        }

        public async Task<Respuesta> Actualizar(Clientes cambiosCliente)
        {
            try
            {
                System.Linq.Expressions.Expression<Func<Clientes, bool>> predicado =
                    e => e.IdClientes.Equals(cambiosCliente.IdClientes);

                Clientes cliente = await _unitWork.Clientes.ConsultarRelacionado("IdClientesNavigation", predicado);

                if (cliente != null)
                {
                    if (cliente.IdentificacionConyuge != null
                        && cliente.IdentificacionConyuge.Equals(cliente.IdClientesNavigation.Identificacion))
                    {
                        throw new Exception(MensajesRespuesta.ERROR_CONYUGES_IDENTIFICACIONES_IGUALES);
                    }

                    cliente.EstadoCivil = cambiosCliente.EstadoCivil;
                    cliente.IdentificacionConyuge = cambiosCliente.IdentificacionConyuge;
                    cliente.NombresConyuge = cambiosCliente.NombresConyuge;
                    cliente.SujetoCredito = cambiosCliente.SujetoCredito;
                    cliente.IdClientesNavigation.Direccion = cambiosCliente.IdClientesNavigation.Direccion;
                    cliente.IdClientesNavigation.Telefono = cambiosCliente.IdClientesNavigation.Telefono;
                    cliente.IdClientesNavigation.Edad =
                        (byte)Calculos.CalcularEdad(cliente.IdClientesNavigation.FechaNacimiento);


                    _unitWork.Clientes.Actualizar(cliente);
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

        public async Task<Respuesta> Agregar(Clientes cliente)
        {
            try
            {
                Func<Personas, bool> predicado =
                    e => e.Identificacion.Equals(cliente.IdClientesNavigation.Identificacion);

                Personas persona = await _unitWork.Personas.ConsultarFiltro(predicado);

                if (persona == null)
                {
                    if (cliente.IdentificacionConyuge != null
                        && cliente.IdentificacionConyuge.Equals(cliente.IdClientesNavigation.Identificacion))
                    {
                        throw new Exception(MensajesRespuesta.ERROR_CONYUGES_IDENTIFICACIONES_IGUALES);
                    }

                    cliente.IdClientesNavigation.Edad =
                        (byte)Calculos.CalcularEdad(cliente.IdClientesNavigation.FechaNacimiento);

                    cliente = await _unitWork.Clientes.Agregar(cliente);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_CLIENTE_YA_EXISTE);
                }
                _resultadoRespuesta = cliente;
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

        public async Task<Respuesta> Consultar(string identificacion)
        {
            try
            {
                System.Linq.Expressions.Expression<Func<Personas, bool>> predicado =
                    e => e.Identificacion.Equals(identificacion);

                Personas persona = await _unitWork.Personas.ConsultarRelacionado("Clientes", predicado);

                if (persona == null)
                {
                    throw new Exception(MensajesRespuesta.WARNING_CLIENTES_NO_ENCONTRADO);
                }
                _resultadoRespuesta = persona;
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

        public async Task<Respuesta> Eliminar(string identificacion)
        {
            try
            {
                System.Linq.Expressions.Expression<Func<Personas, bool>> predicado =
                    e => e.Identificacion.Equals(identificacion);

                Personas persona = await _unitWork.Personas.ConsultarRelacionado("Clientes", predicado);

                if (persona == null)
                {
                    throw new Exception(MensajesRespuesta.WARNING_CLIENTES_NO_ENCONTRADO);
                }

                _unitWork.Clientes.Eliminar(persona.Clientes);
                _unitWork.Personas.Eliminar(persona);
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
