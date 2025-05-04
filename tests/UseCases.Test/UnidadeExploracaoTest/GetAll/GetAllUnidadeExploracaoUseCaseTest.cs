using Agro.Application.UsesCases.UnidadeDeExploracao.GetAll;
using Agro.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.UnidadeExploracaoTest.GetAll
{
    public class GetAllUnidadeExploracaoUseCaseTest
    {
        [Fact]
        public async Task Sucess_With_Pagination()
        {
            MapsterTestSetup.Initialize();

            int page = 1;
            int pageSize = 3;

            var unidades = new List<UnidadeExploracao>
                {
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build()
                };

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                .WithUnidadesExploracao(unidades).Build();

            var useCase = new GetAllUnidadeExploracaoUseCase(readRepository);

            var result = await useCase.Execute(page,pageSize);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(pageSize);
        }

        [Fact]
        public async Task Sucess_Without_Pagination()
        {
            int page = 0;
            int pageSize = 0;

            MapsterTestSetup.Initialize();

            var unidades = new List<UnidadeExploracao>
                {
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build(),
                  UnidadeExploracaoBuilder.Build()
                };

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                .WithUnidadesExploracao(unidades).Build();

            var useCase = new GetAllUnidadeExploracaoUseCase(readRepository);

            var result = await useCase.Execute(page, pageSize);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(unidades.Count);
            result.First().QuantidadeAnimais.ShouldBe(unidades.First().QuantidadeAnimais);
            result.First().CodigoPropriedade.ShouldBe(unidades.First().CodigoPropriedade);
            result.First().CodigoEspecie.ShouldBe(unidades.First().EspecieId);

        }

        [Fact]
        public async Task Sucess_Number_He_Must_Return_Zero()
        {
            int page = 2;
            int pageSize = 3;

            MapsterTestSetup.Initialize();

            var unidades = new List<UnidadeExploracao>{};

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                .WithUnidadesExploracao(unidades).Build();

            var useCase = new GetAllUnidadeExploracaoUseCase(readRepository);

            var result = await useCase.Execute(page, pageSize);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(0);
           

        }
    }
}
