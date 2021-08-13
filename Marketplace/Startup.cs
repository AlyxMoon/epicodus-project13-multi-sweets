using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using Marketplace.Models;
using Marketplace.Models.Database;

namespace Marketplace
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      IConfigurationBuilder builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json");

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      string connection = Configuration["ConnectionStrings:DefaultConnection"];
      ServerVersion version = ServerVersion.AutoDetect(connection);

      services.AddMvc();

      services
        .AddEntityFrameworkMySql()
        .AddDbContext<DatabaseContext>(options => options.UseMySql(connection, version));
        
      services
        .AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
      });
    }

    public void Configure(IApplicationBuilder app)
    {
      app
        .UseDeveloperExceptionPage()
        .UseAuthentication()
        .UseStaticFiles()
        .UseRouting()
        .UseAuthorization()
        .UseEndpoints(routes => {
          routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        })
        .Run(async (context) => {
          await context.Response.WriteAsync("Hello World!");
        });
    }
  }
}