using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using moviecruiser.Data;
using moviecruiser.Data.Persistance;

namespace moviecruiser
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      string constr = Environment.GetEnvironmentVariable("SQLSERVER_MOVIE");
      if (constr == null)
      {
        constr = Configuration.GetConnectionString("MovieConnection"); ;
      }
      //Injecting and configuring DbContext
      services.AddDbContext<MoviesDbContext>(options =>options.UseSqlServer(constr));
      services.AddMvc();
      services.AddScoped<IMoviesDbContext, MoviesDbContext>();
      //Injecting IMovieRepository
      services.AddScoped<IMovieRepository, MovieRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var dbContext = serviceScope.ServiceProvider.GetService<MoviesDbContext>();
        DbSeeder.Seed(dbContext);
      }
      //Enabling Cors
      app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
