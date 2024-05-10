using FormBuilderBLAZOR.Components;
using FormBuilderDTO.DTOs.Config;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderBLAZOR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            #region Binding config settings

            builder.Services.AddOptions<ConnectionStrings>()
                .Bind(builder.Configuration.GetSection(nameof(ConnectionStrings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            #endregion

            builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var dbSettings = serviceProvider.GetRequiredService<IOptionsMonitor<ConnectionStrings>>().CurrentValue;
                options.UseSqlServer(dbSettings.DbConnection);
            });

            builder.Services.AddTransient<ISurveyRepository, SurveyRepository>();
            builder.Services.AddTransient<IControlRepository, ControlRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
