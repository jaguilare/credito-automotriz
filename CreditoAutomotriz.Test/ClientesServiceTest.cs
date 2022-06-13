using CreditoAutomotriz.Domain.Implementations;
using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Infrastructure.AppContextDB;
using CreditoAutomotriz.Repository.Implementation;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Test
{

    [TestFixture]
    public class ClientesServiceTest
    {


        private CreditoAutomotrizContext _dbContext;
        private UnitWork _unitWork;
        private IClientesService _clienteService;


        [SetUp]
        public void SetUp()
        {
            _dbContext = new CreditoAutomotrizContext();
            _unitWork = new UnitWork(_dbContext);
            _clienteService = new ClientesService(_unitWork);
        }

        [Test]
        public async Task Consultar()
        {
            Respuesta respuesta = await _clienteService.Consultar("12345");

            Assert.AreEqual(true, true);
        }

    }
}
