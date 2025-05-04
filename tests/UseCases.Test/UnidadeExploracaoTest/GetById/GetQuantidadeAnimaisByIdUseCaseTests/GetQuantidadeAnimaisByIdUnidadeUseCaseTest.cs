using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetQuantidadeAnimaisByIdUseCase;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.UnidadeExploracaoTest.GetById.GetQuantidadeAnimaisByIdUseCaseTests
{
    public class GetQuantidadeAnimaisByIdUnidadeUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {

            var unidade = UnidadeExploracaoBuilder.Build();

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                .WithUnidadeExploracaoExist(unidade).Build();

            var useCase = new GetQuantidadeAnimaisByIdUnidadeUseCase(readRepository);

            var result = await useCase.Execute(unidade.Id);

            result.ShouldNotBeNull();
            result.QuantidadeAnimais.ShouldBe(unidade.QuantidadeAnimais);

        }

        [Fact]
        public async Task Error_Must_throw_exception_when_Exeploration_Unit_not_found()
        {
            var unidade = UnidadeExploracaoBuilder.Build();

            var readRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder()
                        .WithUnidadeExploracaoInexist().Build();

            var useCase = new GetQuantidadeAnimaisByIdUnidadeUseCase(readRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(unidade.Id));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }
    }
}
