using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Riot.Data.Context;

namespace MatchmakingWorker;

public class Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        logger.LogInformation("Servidor de Matchmaking iniciado correctamente.");

        while (!stoppingToken.IsCancellationRequested)
        {

            var cronometro = Stopwatch.StartNew();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RiotDbContext>();

                logger.LogInformation("--Iniciando ciclo de busqueda--");

                var TotalEnQueue = await dbContext.Players.CountAsync(p => p.IsInQueue, stoppingToken);

                logger.LogInformation("Jugadores buscando paetida actualmente: {count}", TotalEnQueue);
            } 

            cronometro.Stop();

            logger.LogInformation("Ciclo completado en {ms}ms. Esperando sigueinte ciclo", cronometro.ElapsedMilliseconds);
            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task SimularMatchmaking()
    {
        int delay = new Random().Next(500, 2000);
        await Task.Delay(delay);
    }
}
