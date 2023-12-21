using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilter;
using Repositories.Concreate;
using Repositories.EfCore;
using Services;
using Services.Concreate;
using System.Runtime.CompilerServices;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        // neden burayi kullaniyoruz cunku program.cs sisir micak ve eklenti metodlar icin qulaydir
        public static void ConfigureSqlContext(this IServiceCollection services,
              IConfiguration configuration) =>
        
            services.AddDbContext<RepositoryContext>(option =>
           option.UseSqlServer(configuration.GetConnectionString("sqlConnection")));


        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager ,RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager,ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerService, LoggerManager>();

        public static void ConfigureActionResult(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
              services.AddScoped<LogFilterAttribute>();
            services.AddScoped<ValidationFilterAttribute>();
        }    
        
        public static void ConfigureDataShaping(this IServiceCollection services) 
        {
            services.AddScoped<IDateShaping<BookDto>, DataShaping<BookDto>>();
        }
    }

    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config => 
        {
             var systemTextJsonOutputFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

            if (systemTextJsonOutputFormatter is not null) 
            {
                systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.hateoas+json"); 
            }
                   
            var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerInputFormatter>()?.FirstOrDefault();


            if (xmlOutputFormatter is not null)
            {
                xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.hateoas+xml");
            }

        });   
    }    

    
}
