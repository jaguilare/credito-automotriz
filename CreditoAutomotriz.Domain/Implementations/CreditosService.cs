using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Entities.Values;
using CreditoAutomotriz.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CreditoAutomotriz.Domain.Implementations
{
    public class CreditosService : ICreditosService
    {

        private readonly IUnitWork _unitWork;

        private string _mensajeRespuesta;
        private object _resultadoRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        public CreditosService(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _mensajeRespuesta = MensajesRespuesta.OK;
            _resultadoRespuesta = null;
            _codigoRespuesta = ECodigoRespuesta.OK;
        }

        public async Task<Respuesta> Agregar(SolicitudCredito nuevaSolicitudCrecito)
        {

            nuevaSolicitudCrecito.Estado = (short)EEstadosSolicitudCredito.REGISTRADA;
            nuevaSolicitudCrecito.FechaElaboracion = DateTime.Now;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Func<Personas, bool> predicadoPersona =
                                        e => e.IdPersonas.Equals(
                                        nuevaSolicitudCrecito.IdClientes);

                    System.Linq.Expressions.Expression<Func<Clientes, bool>> predicadoCliente =
                        e => e.IdClientes.Equals(nuevaSolicitudCrecito.IdClientes);


                    Clientes cliente =
                        await _unitWork.Clientes.ConsultarRelacionado("IdClientesNavigation", predicadoCliente);

                    if (cliente != null)
                    {
                        //0. Validar si el cliente es sujeto de credito
                        if (cliente.SujetoCredito)
                        {

                            //1. Validar si el cliente posee una solititud activa el dia de hoy.
                            Func<SolicitudCredito, bool> predicadoCreditoActivoHoy =
                                                e =>
                                                e.IdClientes == nuevaSolicitudCrecito.IdClientes
                                                && e.FechaElaboracion.Date >= nuevaSolicitudCrecito.FechaElaboracion.Date
                                                && e.FechaElaboracion.Date <= nuevaSolicitudCrecito.FechaElaboracion.Date
                                                && e.Estado == (short)EEstadosSolicitudCredito.REGISTRADA;

                            SolicitudCredito solicitudCredito =
                                await _unitWork.SolicitudCredito.ConsultarFiltro(predicadoCreditoActivoHoy);

                            if (solicitudCredito == null)
                            {
                                //2. Validar si el auto que desea no está reservado.
                                Func<SolicitudCredito, bool> predicadoSolicitudVehiculoReservado =
                                                    e =>
                                                    e.IdVehiculos == nuevaSolicitudCrecito.IdVehiculos
                                                    && e.Estado == (short)EEstadosSolicitudCredito.REGISTRADA;

                                SolicitudCredito solicitudVehiculoReservado =
                                    await _unitWork.SolicitudCredito.ConsultarFiltro(predicadoSolicitudVehiculoReservado);

                                if (solicitudVehiculoReservado == null)
                                {
                                    //3. Registrar la solicitud de credito y asignar al cliente al patio de vehiculos.
                                    nuevaSolicitudCrecito = await _unitWork.SolicitudCredito.Agregar(nuevaSolicitudCrecito);

                                    //4. Asignar al cliente al patio de vehiculos.
                                    ClientesPatiosAutos clienteAsignacionPatio = new ClientesPatiosAutos
                                    {
                                        IdClientes = nuevaSolicitudCrecito.IdClientes,
                                        IdPatiosAutos = nuevaSolicitudCrecito.IdPatiosAustos,
                                        FechaAsignacion = nuevaSolicitudCrecito.FechaElaboracion
                                    };

                                    await _unitWork.ClientesPatiosAutos.Agregar(clienteAsignacionPatio);
                                    await _unitWork.Guardar();

                                }
                                else
                                {
                                    throw new Exception(
                                        $"{MensajesRespuesta.WARNING_SOLICITUD_CREDITO_VEHICULO_EN_OTRA_SOLICITUD} {nuevaSolicitudCrecito.IdVehiculos}");
                                }
                            }
                            else
                            {
                                throw new Exception(
                                    $"{MensajesRespuesta.WARNING_SOLICITUD_CREDITO_ACTIVA_DIA_HOY} {cliente.IdClientesNavigation.Identificacion}");
                            }
                        }
                        else
                        {
                            throw new Exception(
                                $"{MensajesRespuesta.WARNING_SOLICITUD_CREDITO_CLIENTE_NO_SUJETO_A_CREDITO} {cliente.IdClientesNavigation.Identificacion}");
                        }
                    }
                    else
                    {
                        throw new Exception(
                            $"{MensajesRespuesta.WARNING_CLIENTES_NO_ENCONTRADO}");
                    }

                    scope.Complete();
                    _resultadoRespuesta = nuevaSolicitudCrecito;
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
}
