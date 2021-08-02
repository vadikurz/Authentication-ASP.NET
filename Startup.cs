using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Entities;
using WebApplication.Mappers;
using WebApplication.Models;
using WebApplication.Utils.Constants;


namespace WebApplication
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString(DataBase.ConnectionString));
                })
                .AddIdentity<User, Role>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IMapper<User, SelectableUserViewModel>, UserEntityToSelectableUserViewModelMapper>();
            services.AddSingleton<IMapper<SignUpViewModel, User>, SignUpViewModelToUserEntityMapper>();



            services.AddAuthorization();
            services.ConfigureApplicationCookie(options =>
            {
                options.ReturnUrlParameter = ApplicationCookies.ReturnUrlParameter;
                options.LoginPath = Routes.Account.SignIn;
                options.AccessDeniedPath = Routes.Account.AccessDenied;
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
