using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VisualAcademy.Admin.Areas.Identity;
using VisualAcademy.Admin.Data;
using System.Net.Http;
using VisualAcademy.Admin.Services;
using VisualAcademy.Models;
using VideoAppCore.Models;
using ManufacturerAppCore.Models;
using MachineApp.Models;

namespace VisualAcademy.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<WeatherForecastService>();

            services.AddScoped<HttpClient>(); // MatBlazor
            services.AddScoped<IFileUploadService, FileUploadService>();

            AddDependencyInjectionContainerForIdeaAppCore(services); // Ideas
            AddDependencyInjectionContainerForVideoAppCore(services); // Videos

            // ============================================================================== // 
            // 새로운 DbContext 추가
            services.AddEntityFrameworkSqlServer().AddDbContext<ManufacturerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // DI Container에 주입 
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            // ============================================================================== // 

            AddDependencyInjectionContainerForMachines(services);
        }

        private void AddDependencyInjectionContainerForMachines(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<MachineDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMachineRepository, MachineRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            //services.AddScoped<IMachineMediaRepository, IMachineMediaRepository>();
        }
        private void AddDependencyInjectionContainerForIdeaAppCore(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<IdeaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddTransient<IIdeaRepository, IdeaRepository>();
        }

        private void AddDependencyInjectionContainerForVideoAppCore(IServiceCollection services)
        {
            // 새로운 DbContext 클래스 등록
            services.AddEntityFrameworkSqlServer().AddDbContext<VideoDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // DI Container에 서비스 등록
            //services.AddTransient<IVideoRepositoryAsync, VideoRepositoryEfCoreAsync>();
            //services.AddSingleton<IVideoRepositoryAsync>(new VideoRepositoryAdoNetAsync(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IVideoRepositoryAsync>(new VideoRepositoryDapperAsync(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
