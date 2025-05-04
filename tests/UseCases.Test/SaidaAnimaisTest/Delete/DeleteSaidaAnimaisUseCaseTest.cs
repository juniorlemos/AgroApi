using Agro.Application.UsesCases.SaidaAnimal.Delete;
using Agro.Application.UsesCases.SaidaAnimal.GetById;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions.ExceptionsBase;
using Agro.Exceptions;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.SaidaAnimaisTest.Delete
{
    public class DeleteSaidaAnimaisUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            MapsterTestSetup.Initialize();

            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var unidadadeExploracaoOrigem = saidaAnimais.UnidadeExploracaoOrigem;
            var unidadadeExploracaoSaida = saidaAnimais.UnidadeExploracaoDestino;

            saidaAnimais.QuantidadeAnimais = 7;
            unidadadeExploracaoOrigem.QuantidadeAnimais = 10;
            unidadadeExploracaoSaida.QuantidadeAnimais = 20;


            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadadeExploracaoOrigem.Id, unidadadeExploracaoOrigem)
                                                                                                                .WithUnidadeExploracaoById(unidadadeExploracaoSaida.Id, unidadadeExploracaoSaida).Build(); var deleteOnlyRepository = new SaidaAnimaisDeleteOnlyRepositoryBuilder().WithSaidaAnimaisExist().Build();

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder().WithSaidaAnimaisExist(saidaAnimais).Build();

            var useCase = CreateUseCase(deleteOnlyRepository, saidaAnimaisReadOnlyRepository, unidadeExplorationReadOnlyRepository);

            await useCase.Execute(saidaAnimais.Id);

            true.ShouldBeTrue();
            unidadadeExploracaoOrigem.QuantidadeAnimais.ShouldBe(17);
            unidadadeExploracaoSaida.QuantidadeAnimais.ShouldBe(13);

        }

        [Fact]
        public async Task Error_Must_throw_exception_when_Saida_Animais_not_found()
        {
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var unidadadeExploracao = UnidadeExploracaoBuilder.Build();


            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoExist(unidadadeExploracao).Build();
            var deleteOnlyRepository = new SaidaAnimaisDeleteOnlyRepositoryBuilder().WithSaidaAnimaisExist().Build();
            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder().WithSaidaAnimaisInexist().Build();

            var useCase = CreateUseCase(deleteOnlyRepository, saidaAnimaisReadOnlyRepository, unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id));
            exception.Message.ShouldBe(ResourceMessagesException.ANIMAL_EXIT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Must_throw_exception_when_UnidadeExploracao_Origem_not_found()
        {
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var unidadadeExploracaoOrigem = saidaAnimais.UnidadeExploracaoOrigem;
            var unidadadeExploracaoSaida = saidaAnimais.UnidadeExploracaoDestino;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadadeExploracaoOrigem.Id, null)
                                                                                                    .WithUnidadeExploracaoById(unidadadeExploracaoSaida.Id, unidadadeExploracaoSaida).Build();
            var deleteOnlyRepository = new SaidaAnimaisDeleteOnlyRepositoryBuilder().WithSaidaAnimaisExist().Build();
            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder().WithSaidaAnimaisExist(saidaAnimais).Build();

            var useCase = CreateUseCase(deleteOnlyRepository, saidaAnimaisReadOnlyRepository, unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Must_throw_exception_when_UnidadeExploracao_Saida_not_found()
        {
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var unidadadeExploracaoOrigem = saidaAnimais.UnidadeExploracaoOrigem;
            var unidadadeExploracaoSaida = saidaAnimais.UnidadeExploracaoDestino;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadadeExploracaoOrigem.Id, unidadadeExploracaoOrigem)
                                                                                                       .WithUnidadeExploracaoById(unidadadeExploracaoSaida.Id, null).Build();
            
            var deleteOnlyRepository = new SaidaAnimaisDeleteOnlyRepositoryBuilder().WithSaidaAnimaisExist().Build();
            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder().WithSaidaAnimaisExist(saidaAnimais).Build();


            var useCase = CreateUseCase(deleteOnlyRepository, saidaAnimaisReadOnlyRepository, unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Throw_When_Quantity_Exceeds_Available_Animals_In_Error_Throw_When_Quantity_Exceeds_Available_Animals_In_UnidadeExploracao()
        {
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            var unidadadeExploracaoOrigem = saidaAnimais.UnidadeExploracaoOrigem;
            var unidadadeExploracaoSaida = saidaAnimais.UnidadeExploracaoDestino;

            saidaAnimais.QuantidadeAnimais = 12;
            unidadadeExploracaoSaida.QuantidadeAnimais = 5;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadadeExploracaoOrigem.Id, unidadadeExploracaoOrigem)
                                                                                                       .WithUnidadeExploracaoById(unidadadeExploracaoSaida.Id, unidadadeExploracaoSaida).Build();

            var deleteOnlyRepository = new SaidaAnimaisDeleteOnlyRepositoryBuilder().WithSaidaAnimaisExist().Build();
            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder().WithSaidaAnimaisExist(saidaAnimais).Build();


            var useCase = CreateUseCase(deleteOnlyRepository, saidaAnimaisReadOnlyRepository, unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<BusinessException>(() => useCase.Execute(saidaAnimais.Id));
            exception.Message.ShouldBe(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);
        }

        private static DeleteSaidaAnimaisUseCase CreateUseCase(
            ISaidaAnimaisDeleteOnlyRepository deleteOnlyRepository,
            ISaidaAnimaisReadOnlyRepository saidaAnimaisReadOnlyRepository,
            IUnidadeExploracaoReadOnlyRepository unidadeExplorationReadOnlyRepository)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var unidadeExploracaoUpdateOnlyRepository = UnidadeExploracaoUpdateOnlyRepository.Build();
            return new DeleteSaidaAnimaisUseCase(deleteOnlyRepository, unitOfWork, unidadeExplorationReadOnlyRepository,
                                                 saidaAnimaisReadOnlyRepository, unidadeExploracaoUpdateOnlyRepository);
        }
    }
}
