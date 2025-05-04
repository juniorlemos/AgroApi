using Agro.Communication.Request;
using Agro.Communication.Response;
using Agro.Domain.Entities;
using Mapster;

namespace CommonTestUtilities.Mapper
{
    public static class MapsterTestSetup
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
                return;


            TypeAdapterConfig<RequestRegisterUnidadeExploracaoJson, UnidadeExploracao>
                .NewConfig()
                .Map(dest => dest.EspecieId, src => src.CodigoEspecie)
                .Map(dest => dest.QuantidadeAnimais, src => src.QuantidadeAnimais)
                .Map(dest => dest.CodigoPropriedade, src => src.CodigoPropriedade);


            TypeAdapterConfig<UnidadeExploracao, ResponseUnidadeExploracaoJson>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.QuantidadeAnimais, src => src.QuantidadeAnimais)
                .Map(dest => dest.CodigoPropriedade, src => src.CodigoPropriedade)
                .Map(dest => dest.CodigoEspecie, src => src.EspecieId);

            _initialized = true;
        }
    }
}
