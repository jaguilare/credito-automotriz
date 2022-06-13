//using CreditoAutomotriz.Domain.Implementations;
//using CreditoAutomotriz.Domain.Interfaces;
//using CreditoAutomotriz.Entities.App;
//using CreditoAutomotriz.Infrastructure.AppContextDB;
//using CreditoAutomotriz.Repository.Implementation;
//using CreditoAutomotriz.Repository.Implementations;
//using CreditoAutomotriz.Repository.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Test
{
    [TestFixture]
    public class CargaInicialTest
    {
        //private IFilesService _filesHandler;
        //private IFilesRepository<object> _filesRepository;
        //private IClientesService _clientesService;
        //private IClientesRepository _clientesRepository;
        //private IPersonasRepository _personasRepository;
        //private CreditoAutomotrizContext _dbContext;

        [SetUp]
        public void SetUp()
        {

            //CreditoAutomotrizContext dbContext = new CreditoAutomotrizContext();

            //_filesRepository = new FilesRepository<object>();
            //_clientesRepository = new ClientesRepository(dbContext);
            //_personasRepository = new PersonasRepository(dbContext);
            //_clientesService = new ClientesService(_clientesRepository, _personasRepository);

            //_filesHandler = new FilesService(_filesRepository, _clientesService);

        }

        [Test]
        public void CargaInicialArchivoCsv()
        {

            //Task<Respuesta> result = _filesHandler.LoadCsv();

            //Assert.AreEqual(ECodigoRespuesta.OK, result.Result.Codigo);
        }
    }
}
