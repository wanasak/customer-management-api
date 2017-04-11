using Microsoft.EntityFrameworkCore;
using CustomerManagement.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Security.Claims;

namespace CustomerManagement.Api
{
	public class Startup
	{
		bool useInMemoryProvider = false;
		string sqlConnectionString = string.Empty;

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
			    .SetBasePath(env.ContentRootPath)
			    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
			    .AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			sqlConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
			useInMemoryProvider = bool.Parse(Configuration["AppSettings:InMemoryProvider"]);
			services.AddDbContext<CustomerManagementContext>(options =>
			{
				switch (useInMemoryProvider)
				{
					case true:
						options.UseInMemoryDatabase();
						break;
					default:
						options.UseSqlServer(sqlConnectionString,
					b => b.MigrationsAssembly("Scheduler.API"));
						break;
				}
			});

			// Repositories
			services.AddScoped<IUserRepositories, UserRepositories>();
			services.AddScoped<ICustomerRepositories, CustomerRepositories>();
			services.AddScoped<IOrderRepositories, OrderRepositories>();
			services.AddScoped<IStateRepositories, StateRepositories>();

			// Add framework services.
			services.AddMvc()
				.AddJsonOptions(opts =>
				{
					// Force Camel Case to JSON
					opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				});

			services.AddAuthorization(options =>
				options.AddPolicy("Admin", policy =>
				{
					policy.RequireClaim(ClaimTypes.Role, "Admin");
				})
			 );

			services.AddAuthentication();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			// Addd cookie authen
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true
			});

			// Add MVC to the request pipeline.
			app.UseCors(builder =>
				builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());

			//loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			//loggerFactory.AddDebug();

			app.UseMvc();

			CustomerManagementInitializer.Initialize(app.ApplicationServices);
		}
	}
}
