using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Entities.MapFiles;
using CreditoAutomotriz.Repository.Interfaces;
using CreditoAutomotriz.Repository.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Implementations
{
    public class ArchivosService : IArchivosService
    {

        private readonly IUnitWork _unitWork;
        private readonly IArchivosRepository<object> _archivosRepository;

        public ArchivosService(
            IUnitWork unitWork, IArchivosRepository<object> archivosRepository)
        {
            _unitWork = unitWork;
            _archivosRepository = archivosRepository;
        }


        public async Task<Respuesta> CargarCsv()
        {
            object resultadoRespuesta = null;
            string mensajeRespuesta = MensajesRespuesta.OK;
            ECodigoRespuesta codigoRespuesta = ECodigoRespuesta.OK;

            Task[] taskArray = null;

            try
            {
                // cargar cada archivo en una tarea diferente
                taskArray = new Task[]{
                    Task<List<ClientesDtoFile>>.Factory.StartNew(() =>
                    {
                        return _archivosRepository.CargarCsv("clientes.csv", new ClientesMap());
                    }),
                    Task<List<EjecutivosDtoFile>>.Factory.StartNew(() =>
                    {
                        return _archivosRepository.CargarCsv("ejecutivos.csv", new EjecutivosMap());
                    }),
                    Task<List<MarcasVehiculosDtoFile>>.Factory.StartNew(() =>
                    {
                        return _archivosRepository.CargarCsv("marcas.csv", new MarcasMap());
                    })
                };

                Task.WaitAll(taskArray);
            }
            catch (Exception exc)
            {
                mensajeRespuesta = exc.Message;
                codigoRespuesta = ECodigoRespuesta.ERROR;
                resultadoRespuesta = null;
                Console.WriteLine($"\n{mensajeRespuesta}\n");
                throw new Exception(mensajeRespuesta);
            }


            // en caso de existir archivos duplicados, presentar un error
            var recordsClientes = (taskArray[0] as Task<List<ClientesDtoFile>>).Result;
            var recordsClientesRepeat = recordsClientes.GroupBy(r => new { r.Identificacion }).Where(r => r.Count() > 1);

            if (recordsClientesRepeat.Any())
            {
                throw new Exception(MensajesRespuesta.ERROR_REGISTROS_CLIENTES_CSV_REPETIDOS);
            }
            Console.WriteLine($"\n{MensajesRespuesta.OK_REGISTROS_CLIENTES_CSV_GUARDADOS}\n");

            List<Clientes> clientes = ClientesDtoFile.ToClientes(recordsClientes);

            foreach (var c in clientes)
            {
                try
                {
                    Func<Personas, bool> predicado = p =>
                    p.Identificacion.Equals(c.IdClientesNavigation.Identificacion);
                    Personas auxPersona = await _unitWork.Personas.ConsultarFiltro(predicado);
                    if (auxPersona == null)
                    {
                        await _unitWork.Clientes.Agregar(c);
                        int clientesAfectados = await _unitWork.Guardar();
                    }
                    else
                    {
                        throw new Exception(
                            $"{MensajesRespuesta.WARNING_CLIENTE_YA_EXISTE} " +
                            $"con identificacion " +
                            $"{c.IdClientesNavigation.Identificacion}");
                    }

                }
                catch (Exception exc)
                {
                    Console.WriteLine($"\n{exc.Message}\n");
                }
            }
            // VALIDAR SI EXISTEN EN LA BASE DE DATOS, SINO, INSERTARLOS; CASO CONTRARIO INFORMAR AL USUARIO


            var recordsEjecutivos = (taskArray[1] as Task<List<EjecutivosDtoFile>>).Result;
            var recordsEjecutivosRepeat = recordsEjecutivos.GroupBy(e => new { e.Identificacion }).Where(r => r.Count() > 1);
            if (recordsEjecutivosRepeat.Any())
            {
                throw new Exception(MensajesRespuesta.ERROR_REGISTROS_EJECUTIVOS_CSV_REPETIDOS);
            }
            Console.WriteLine($"\n{MensajesRespuesta.OK_REGISTROS_EJECUTIVOS_CSV_GUARDADOS}\n");

            List<Ejecutivos> ejecutivos = EjecutivosDtoFile.ToEjecutivos(recordsEjecutivos);
            foreach (var e in ejecutivos)
            {
                try
                {
                    Func<Personas, bool> predicado = p =>
                    p.Identificacion.Equals(e.IdEjecutivosNavigation.Identificacion);
                    Personas auxPersona = await _unitWork.Personas.ConsultarFiltro(predicado);
                    if (auxPersona == null)
                    {
                        await _unitWork.Ejecutivos.Agregar(e);
                        int clientesAfectados = await _unitWork.Guardar();
                    }
                    else
                    {
                        throw new Exception(
                            $"{MensajesRespuesta.WARNING_REGISTROS_EJECUTIVOS_DB_REPETIDOS} " +
                            $"con identificacion " +
                            $"{e.IdEjecutivosNavigation.Identificacion}");
                    }

                }
                catch (Exception exc)
                {
                    Console.WriteLine($"\n{exc.Message}\n");
                }
            }
            // VALIDAR SI EXISTEN EN LA BASE DE DATOS, SINO, INSERTARLOS; CASO CONTRARIO INFORMAR AL USUARIO



            var recordsMarcas = (taskArray[2] as Task<List<MarcasVehiculosDtoFile>>).Result;
            var recordsMarcasRepeat = recordsMarcas.GroupBy(m => m.Marca).Where(m => m.Count() > 1);
            if (recordsMarcasRepeat.Any())
            {
                throw new Exception(MensajesRespuesta.ERROR_REGISTROS_MARCAS_CSV_REPETIDAS);
            }
            Console.WriteLine($"\n{MensajesRespuesta.OK_REGISTROS_MARCAS_CSV_GUARDADOS}\n");

            List<MarcasVehiculos> marcasVehiculos = MarcasVehiculosDtoFile.ToMarcasVehiculos(recordsMarcas);
            foreach (var m in marcasVehiculos)
            {
                try
                {
                    Func<MarcasVehiculos, bool> predicado = p =>
                    p.Marca.Equals(m.Marca);
                    MarcasVehiculos auxMarca = await _unitWork.MarcasVehiculos.ConsultarFiltro(predicado);
                    if (auxMarca == null)
                    {
                        await _unitWork.MarcasVehiculos.Agregar(m);
                        int marcasAfectadas = await _unitWork.Guardar();
                    }
                    else
                    {
                        throw new Exception(
                            $"{MensajesRespuesta.WARNING_REGISTROS_MARCAS_VEHICULOS_DB_REPETIDOS} " +
                            $"con nombre " +
                            $"{m.Marca}");
                    }

                }
                catch (Exception exc)
                {
                    Console.WriteLine($"\n{exc.Message}\n");
                }
            }
            // VALIDAR SI EXISTEN EN LA BASE DE DATOS, SINO, INSERTARLOS; CASO CONTRARIO INFORMAR AL USUARIO




            return new Respuesta()
            {
                Codigo = codigoRespuesta,
                Mensaje = mensajeRespuesta,
                Resultado = resultadoRespuesta
            };
        }
    }
}
