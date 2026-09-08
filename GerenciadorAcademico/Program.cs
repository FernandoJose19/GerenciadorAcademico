using GerenciadorAcademico.Repositories;
using GerenciadorAcademico.Services;

namespace GerenciadorAcademico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Registrando nosso serviço como Singleton (uma única instância para toda a aplicação)

            builder.Services.AddSingleton<JsonFileStore>();
            builder.Services.AddScoped<IProjetoRepository, ProjetoJsonRepository>();
            builder.Services.AddScoped<IProfessorRepository, ProfessorJsonRepository>();
            builder.Services.AddScoped<IProjetoService, ProjetoService>();

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
