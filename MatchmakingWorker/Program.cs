using MatchmakingWorker;
using Microsoft.EntityFrameworkCore;
using Riot.Data.Context;

var builder = Host.CreateApplicationBuilder(args);

var Conexion = builder.Configuration.GetConnectionString("MiConexion");

builder.Services.AddDbContext<RiotDbContext>(options =>
options.UseSqlServer(Conexion));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
