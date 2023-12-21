using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EfCore;

namespace WebApi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
       // web api icina context ekleyoruz ve calistiryoruz  
        public RepositoryContext CreateDbContext(string[] args)
        {
            // configuration

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //DbContextOptions

            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                 prj => prj.MigrationsAssembly("WebApi"));
                 
            

            return new RepositoryContext(builder.Options);


        }
    }
}
