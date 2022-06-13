//using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Hosts
{
    public class AppHost : IHostedService
    {
        private readonly IHostApplicationLifetime _lifeTime;

        private readonly IArchivosService _archivosService;

        public AppHost(IArchivosService archivosService, IHostApplicationLifetime lifeTime)
        {
            _archivosService = archivosService;
            _lifeTime = lifeTime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                Respuesta respuesta = await _archivosService.CargarCsv();
                if (respuesta.Codigo == ECodigoRespuesta.ERROR)
                {
                    Console.WriteLine($"\nSERVICIO APAGADO POR => {respuesta.Mensaje}\n");
                    _lifeTime.StopApplication();
                }
            });
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Task.FromResult(0);
            });
        }
    }
}
