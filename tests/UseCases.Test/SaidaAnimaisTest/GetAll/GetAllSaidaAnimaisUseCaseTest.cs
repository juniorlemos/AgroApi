using Agro.Application.UsesCases.SaidaAnimal.GetAll;
using Agro.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.SaidaAnimaisTest.GetAll
{
    public class GetAllSaidaAnimaisUseCaseTest
    {
        [Fact]
        public async Task Sucess_With_Pagination()
        {
            MapsterTestSetup.Initialize();

            int page = 1;
            int pageSize = 3;

            var saidasAnimais = new List<SaidaAnimais>
                {
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build()
                };

            var readRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidasAnimaisExist(saidasAnimais).Build();

            var useCase = new GetAllSaidaAnimaisUseCase(readRepository);

            var result = await useCase.Execute(page, pageSize);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(pageSize);
        }

        [Fact]
        public async Task Sucess_Without_Pagination()
        {
            int page = 0;
            int pageSize = 0;

            MapsterTestSetup.Initialize();

            var saidasAnimais = new List<SaidaAnimais>
                {
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build(),
                  SaidaAnimaisBuilder.Build()
                };

            var readRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidasAnimaisExist(saidasAnimais).Build();

            var useCase = new GetAllSaidaAnimaisUseCase(readRepository);

            var result = await useCase.Execute(page, pageSize);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(saidasAnimais.Count);
            result.First().QuantidadeAnimais.ShouldBe(saidasAnimais.First().QuantidadeAnimais);
            result.First().DataSaida.ShouldBe(saidasAnimais.First().DataSaida);
            result.First().CodigoUEPOrigem.ShouldBe(saidasAnimais.First().CodigoUEPOrigem);
            result.First().CodigoUEPSaida.ShouldBe(saidasAnimais.First().CodigoUEPSaida);

        }

        [Fact]
        public async Task Sucess_Number_He_Must_Return_Zero()
        {
            int page = 2;
            int pageSize = 3;

            MapsterTestSetup.Initialize();

            var saidasAnimais = new List<SaidaAnimais> { };

            var readRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidasAnimaisExist(saidasAnimais).Build();

            var useCase = new GetAllSaidaAnimaisUseCase(readRepository);

            var result = await useCase.Execute(page, pageSize);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(0);
        }
    }
}
