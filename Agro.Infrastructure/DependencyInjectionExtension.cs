using Agro.Domain.Repositories;
using Agro.Domain.Repositories.Especie;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Infrastructure.DataAcess;
using Agro.Infrastructure.DataAcess.Repositories;
using Agro.Infrastructure.Extensions;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agro.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext_SqlServer(services, configuration);
            AddFluentMigrator_SqlServer(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddDbContext<AgroDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }
        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(DependencyInjectionExtension).Assembly).For.Migrations();
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISaidaAnimaisWriteOnlyRepository, SaidaAnimaisRepository>();
            services.AddScoped<ISaidaAnimaisReadOnlyRepository, SaidaAnimaisRepository>();
            services.AddScoped<ISaidaAnimaisUpdateOnlyRepository, SaidaAnimaisRepository>();
            services.AddScoped<ISaidaAnimaisDeleteOnlyRepository, SaidaAnimaisRepository>();
            
            services.AddScoped<IUnidadeExploracaoWriteOnlyRepository, UnidadeExploracaoRepository>();
            services.AddScoped<IUnidadeExploracaoReadOnlyRepository, UnidadeExploracaoRepository>();
            services.AddScoped<IUnidadeExploracaoDeleteOnlyRepository, UnidadeExploracaoRepository>();
            services.AddScoped<IUnidadeExploracaoUpdateOnlyRepository, UnidadeExploracaoRepository>();


            services.AddScoped<IEspecieReadOnlyRepository, EspecieRepository>();
        }
    }
}
