using GerenciadorAcademico.Repositories;
using GerenciadorAcademico.Services;
using GerenciadorAcademico.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAcademico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("AcademicoConnection")?? throw new InvalidOperationException(
            "A conexão AcademicoConnection não foi configurada.");

            builder.Services.AddDbContext<AcademicoContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<JsonFileStore>();

            builder.Services.AddScoped<
                IProjetoRepository,
                ProjetoEfRepository>();

            builder.Services.AddScoped<
                IProfessorRepository,
                ProfessorEfRepository>();

            builder.Services.AddScoped<
                IProjetoService,
                ProjetoService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Projetos}/{action=index}/{id?}")
                .WithStaticAssets();

            app.Run();
            
        }
    }
}
