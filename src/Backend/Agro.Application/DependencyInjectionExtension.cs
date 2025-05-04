using Agro.Application.Services.Mapster;
using Agro.Application.UsesCases.SaidaAnimal.Delete;
using Agro.Application.UsesCases.SaidaAnimal.GetAll;
using Agro.Application.UsesCases.SaidaAnimal.GetById;
using Agro.Application.UsesCases.SaidaAnimal.Register;
using Agro.Application.UsesCases.SaidaAnimal.Update;
using Agro.Application.UsesCases.UnidadeDeExploracao.Delete;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetAll;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetQuantidadeAnimaisByIdUseCase;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetUnidadeExploracaoByIdUseCase;
using Agro.Application.UsesCases.UnidadeDeExploracao.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agro.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCases(services);
            AutoMapping.RegisterMappings();
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUnidadeExploracaoUseCase, RegisterUnidadeExploracaoUseCase>();
            services.AddScoped<IGetUnidadeExploracaoByIdUseCase, GetUnidadeExploracaoByIdUseCase>();
            services.AddScoped<IGetQuantidadeAnimaisByIdUnidadeUseCase, GetQuantidadeAnimaisByIdUnidadeUseCase>();
            services.AddScoped<IGetAllUnidadeExploracaoUseCase, GetAllUnidadeExploracaoUseCase>();
            services.AddScoped<IDeleteUnidadeExploracaoUseCase, DeleteUnidadeExploracaoUseCase>();
            services.AddScoped<IRegisterSaidaAnimaisUseCase, RegisterSaidaAnimaisUseCase>();
            services.AddScoped<IGetSaidaAnimaisByIdUseCase, GetSaidaAnimaisByIdUseCase>();
            services.AddScoped<IUpdateSaidaAnimaisUseCase, UpdateSaidaAnimaisUseCase>();
            services.AddScoped<IGetAllSaidaAnimaisUseCase, GetAllSaidaAnimaisUseCase>();
            services.AddScoped<IDeleteSaidaAnimaisUseCase, DeleteSaidaAnimaisUseCase>();
        }
    }
}
