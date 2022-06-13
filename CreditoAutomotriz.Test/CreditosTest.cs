using CreditoAutomotriz.Domain.Implementations;
using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using CreditoAutomotriz.Infrastructure.AppContextDB;
using CreditoAutomotriz.Repository.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Test
{

    [TestFixture]
    public class CreditosTest
    {

        private CreditoAutomotrizContext _dbContext;
        private UnitWork _unitWork;
        private ICreditosService _creditosService;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new CreditoAutomotrizContext();
            _unitWork = new UnitWork(_dbContext);
            _creditosService = new CreditosService(_unitWork);
        }

        [Test]
        public async Task SolititarCreditoTest()
        {

            SolicitudCredito solicitud = new SolicitudCredito
            {
                FechaElaboracion = DateTime.Now,
                IdClientes = 2079,
                IdPatiosAustos = 2,
                IdEjecutivos = 2081,
                IdVehiculos = 1008,
                MesesPlazo = 12,
                Cuotas = 600,
                Entrada = 10000,
                Observacion = "Primer auto"
            };

            Respuesta respuesta = await _creditosService.Agregar(solicitud);

            Assert.AreEqual(ECodigoRespuesta.OK, respuesta.Codigo, respuesta.Mensaje);
        }
    }
}
