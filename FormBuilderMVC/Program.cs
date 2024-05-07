using FormBuilderDTO.DTOs.Config;
using FormBuilderSharedService.DbContexts;
using FormBuilderSharedService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FormBuilderMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
            builder.Services.AddTransient<IInputRepository, InputRepository>();
            builder.Services.AddTransient<IControlRepository, ControlRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
