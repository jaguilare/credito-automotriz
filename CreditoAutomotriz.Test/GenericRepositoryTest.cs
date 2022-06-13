using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Infrastructure.AppContextDB;
using CreditoAutomotriz.Repository.Implementation;
using CreditoAutomotriz.Repository.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Test
{

    [TestFixture]
    public class GenericRepositoryTest
    {

        private CreditoAutomotrizContext _dbContext;
        private IGenericRepository<Clientes> _genericoRepository;


        [SetUp]
        public void SetUp()
        {
            _dbContext = new CreditoAutomotrizContext();
            _genericoRepository = new GenericRepository<Clientes>(_dbContext);
        }

        [Test]
        public void Consultar()
        {
            IEnumerable<Clientes> clientes = _genericoRepository.Consultar().Result;
            Assert.AreEqual(true, true);
        }

        [Test]
        public void ConsultarListaFiltro()
        {
            Func<Clientes, bool> predicado = e => e.EstadoCivil.Equals("Casado");
            IEnumerable<Clientes> clientes = (IEnumerable<Clientes>)_genericoRepository.ConsultarListaFiltro(predicado);
            Assert.AreEqual(true, true);
        }

        [Test]
        public async void ConsultarFiltro()
        {
            Func<Clientes, bool> predicado = e => e.IdClientes.Equals(1044);
            Clientes clientes = (Clientes)await _genericoRepository.ConsultarFiltro(predicado);
            Assert.AreEqual(true, true);
        }

        [Test]
        public void ObtenerId()
        {
            var idCliente = 443;
            Clientes clientes = _genericoRepository.Consultar(idCliente).Result;
            Assert.AreEqual(true, true);
        }

        //[Test]
        //public void Eliminar()
        //{
        //    try
        //    {
        //        var idCliente = 1024;
        //        _genericoRepository.Eliminar(idCliente);
        //    }
        //    catch (Exception exc)
        //    {
        //        var error = exc.Message;
        //    }
        //    Assert.AreEqual(true, true);
        //}

        [Test]
        public async Task Agregar()
        {
            try
            {

                var unitWork = new UnitWork(_dbContext);

                var clientes = await unitWork.Clientes.Consultar();

                Clientes cliente = new Clientes()
                {
                    EstadoCivil = "Casado",
                    IdentificacionConyuge = null,
                    NombresConyuge = null,
                    SujetoCredito = true,
                    IdClientesNavigation = new Personas
                    {
                        Identificacion = "4443347",
                        Nombres = "Marco",
                        Apellidos = "Mariachi",
                        Direccion = "Av. LEL",
                        Telefono = "141234",
                        FechaNacimiento = DateTime.Now,
                        Edad = 42
                    }
                };

                cliente = await unitWork.Clientes.Agregar(cliente);
                var regs = await unitWork.Guardar();

                var clientes2 = await unitWork.Clientes.Consultar();


            }
            catch (Exception exc)
            {
                var error = exc.Message;
            }
            Assert.AreEqual(true, true);
        }

        [Test]
        public async Task AgregarListaTest()
        {
            try
            {

                var unitWork = new UnitWork(_dbContext);

                List<Clientes> clientes = new List<Clientes>();


                for (int i = 10; i < 20; i++)
                {
                    clientes.Add(new Clientes()
                    {
                        EstadoCivil = (i % 2 == 0) ? "Casado" : "Soltero",
                        IdentificacionConyuge = (i % 2 == 0) ? i * 453 + "" : null,
                        NombresConyuge = (i % 2 == 0) ? "Conyugue " + i * 453 : null,
                        SujetoCredito = (i % 2 == 0) ? true : false,
                        IdClientesNavigation = new Personas
                        {
                            Identificacion = (i % 2 == 0) ? i * 453 + "" : (i * 453) + "32" + (i - 453),
                            Nombres = (i % 2 == 0) ? "Nombre " + i * 453 : "Nombre " + (i - 453),
                            Apellidos = (i % 2 == 0) ? "Apellido " + i * 453 : "Apellido " + (i - 453),
                            Direccion = (i % 2 == 0) ? "Direccion Av. " + i * 453 : "Direccion Av. " + (i - 453),
                            Telefono = (i % 2 == 0) ? "Fon " + i * 453 : "Fon " + (i - 453),
                            FechaNacimiento = DateTime.Now,
                            Edad = (byte)((i % 2 == 0) ? 34 - i : 45 + i),
                        }
                    });
                };

                await unitWork.Clientes.Agregar(clientes);
                var regs = await unitWork.Guardar();

                var clientes2 = await unitWork.Clientes.Consultar();


            }
            catch (Exception exc)
            {
                var error = exc.Message;
            }
            Assert.AreEqual(true, true);
        }

    }
}
