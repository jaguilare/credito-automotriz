using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
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
    public class PatiosAutosService : IPatiosAutosService
    {

        private readonly IUnitWork _unitWork;

        private string _mensajeRespuesta;
        private object _resultadoRespuesta;
        private ECodigoRespuesta _codigoRespuesta;

        public PatiosAutosService(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            _mensajeRespuesta = MensajesRespuesta.OK;
            _resultadoRespuesta = null;
            _codigoRespuesta = ECodigoRespuesta.OK;
        }


        public async Task<Respuesta> Actualizar(PatiosAutos cambioPatioAutos)
        {
            try
            {
                Func<PatiosAutos, bool> predicado =
                    e => e.IdPationsAutos.Equals(cambioPatioAutos.IdPationsAutos);

                PatiosAutos patioAutos = await _unitWork.PatiosAutos.ConsultarFiltro(predicado);

                if (patioAutos != null)
                {
                    patioAutos.Nombre = cambioPatioAutos.Nombre;
                    patioAutos.Direccion = cambioPatioAutos.Direccion;
                    patioAutos.Telefono = cambioPatioAutos.Telefono;
                    patioAutos.NroPuntoVenta = cambioPatioAutos.NroPuntoVenta;

                    _unitWork.PatiosAutos.Actualizar(patioAutos);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_PATIO_AUTOS_NO_ENCONTRADO);
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

        public async Task<Respuesta> Agregar(PatiosAutos nuevoPatioAutos)
        {
            try
            {
                Func<PatiosAutos, bool> predicado =
                    e => e.NroPuntoVenta.Equals(nuevoPatioAutos.NroPuntoVenta);

                PatiosAutos patioAutos = await _unitWork.PatiosAutos.ConsultarFiltro(predicado);

                if (patioAutos == null)
                {
                    nuevoPatioAutos = await _unitWork.PatiosAutos.Agregar(nuevoPatioAutos);
                    await _unitWork.Guardar();
                }
                else
                {
                    throw new Exception(MensajesRespuesta.WARNING_PATIO_AUTOS_YA_EXISTE);
                }
                _resultadoRespuesta = nuevoPatioAutos;
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

        public async Task<Respuesta> Consultar(string nombre)
        {
            try
            {
                Func<PatiosAutos, bool> predicado =
                    e => e.Nombre.ToLower().Equals(nombre.ToLower());

                PatiosAutos patioAutos = await _unitWork.PatiosAutos.ConsultarFiltro(predicado);

                if (patioAutos == null)
                {
                    throw new Exception(MensajesRespuesta.WARNING_PATIO_AUTOS_NO_ENCONTRADO);
                }
                _resultadoRespuesta = patioAutos;
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

        public async Task<Respuesta> Eliminar(int idPatiosAutos)
        {
            try
            {
                Func<PatiosAutos, bool> predicado =
                    e => e.IdPationsAutos == idPatiosAutos;

                PatiosAutos patioAutos = await _unitWork.PatiosAutos.ConsultarFiltro(predicado);

                if (patioAutos == null)
                {
                    throw new Exception(MensajesRespuesta.WARNING_PATIO_AUTOS_NO_ENCONTRADO);
                }

                _unitWork.PatiosAutos.Eliminar(patioAutos);
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
