using System.Text.Json.Serialization;

using GerenciadorAcademicoAPI.Data;
using GerenciadorAcademicoAPI.Repositories;
using GerenciadorAcademicoAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAcademicoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connectionString = builder.Configuration
            .GetConnectionString("AcademicoConnection")
            ?? throw new InvalidOperationException(
                "A conexão AcademicoConnection não foi configurada.");

            builder.Services.AddDbContext<AcademicoContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProjetoRepository, ProjetoEfRepository>();

            builder.Services.AddScoped<IProfessorRepository, ProfessorEfRepository>();

            builder.Services.AddScoped<IProjetoService, ProjetoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
