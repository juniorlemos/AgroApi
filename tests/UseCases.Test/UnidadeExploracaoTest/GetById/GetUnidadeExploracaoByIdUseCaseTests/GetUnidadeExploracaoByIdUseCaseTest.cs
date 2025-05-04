using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetQuantidadeAnimaisByIdUseCase;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetUnidadeExploracaoByIdUseCase;
using Agro.Exceptions.ExceptionsBase;
using Agro.Exceptions;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.UnidadeExploracaoTest.GetById.GetUnidadeExploracaoByIdUseCaseTests
{
    public class GetUnidadeExploracaoByIdUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            MapsterTestSetup.Initialize();
            var unidade = UnidadeExploracaoBuilder.Build();

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                .WithUnidadeExploracaoExist(unidade).Build();

            var useCase = new GetUnidadeExploracaoByIdUseCase(readRepository);

            var result = await useCase.Execute(unidade.Id);

            result.ShouldNotBeNull();
            result.QuantidadeAnimais.ShouldBe(unidade.QuantidadeAnimais);
            result.CodigoPropriedade.ShouldBe(unidade.CodigoPropriedade);
            result.CodigoEspecie.ShouldBe(unidade.EspecieId);
        }

        [Fact]
        public async Task Error_Must_throw_exception_when_Exeploration_Unit_not_found()
        {
            var unidade = UnidadeExploracaoBuilder.Build();

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                        .WithUnidadeExploracaoInexist().Build();

            var useCase = new GetUnidadeExploracaoByIdUseCase(readRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(unidade.Id));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }
    }
}
