using Agro.Application.UsesCases.SaidaAnimal.GetById;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.SaidaAnimaisTest.GetById
{
    public class GetSaidaAnimaisByIdUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            MapsterTestSetup.Initialize();
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var readRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var useCase = new GetSaidaAnimaisByIdUseCase(readRepository);

            var result = await useCase.Execute(saidaAnimais.Id);

            result.ShouldNotBeNull();
            result.DataSaida.ShouldBe(saidaAnimais.DataSaida);
            result.QuantidadeAnimais.ShouldBe(saidaAnimais.QuantidadeAnimais);
            result.CodigoUEPOrigem.ShouldBe(saidaAnimais.CodigoUEPOrigem);
            result.CodigoUEPSaida.ShouldBe(saidaAnimais.CodigoUEPSaida);
        }

        [Fact]
        public async Task Error_Must_throw_exception_when_Saida_Animais_not_found()
        {
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var readRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                        .WithSaidaAnimaisInexist().Build();

            var useCase = new GetSaidaAnimaisByIdUseCase(readRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id));
            exception.Message.ShouldBe(ResourceMessagesException.ANIMAL_EXIT_NOT_FOUND);
        }
    }
}

