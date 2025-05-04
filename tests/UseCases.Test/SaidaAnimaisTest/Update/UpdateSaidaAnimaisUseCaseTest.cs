using Agro.Application.UsesCases.SaidaAnimal.Update;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions.ExceptionsBase;
using Agro.Exceptions;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using Shouldly;

namespace UseCases.Test.SaidaAnimaisTest.Update
{
    public class UpdateSaidaAnimaisUseCaseTest
    {
        [Fact]
        public async Task Sucess_Update_Quantities_from_UnidadeOrigem_to_UnidadeSaida()
        {
            MapsterTestSetup.Initialize();

            var quantidadeUnidadeOrigem = 5;
            var quantidadeUnidadeSaida = 7;
            var requestQuantidadeAnimais = 4;
            var saidaAnimaisQuantidade = 2;

            var quantidadeUnidadeOrigemSaidaEsperada = 3;
            var quantidadeUnidadeSaidaSaidaEsperada = 9;
            var saidaAnimaisQuantidadeSaidaEsperada = 4;


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();
            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            saidaAnimais.QuantidadeAnimais = saidaAnimaisQuantidade;
            request.QuantidadeAnimais = requestQuantidadeAnimais;
            unidadeOrigem.QuantidadeAnimais = quantidadeUnidadeOrigem;
            unidadeSaida.QuantidadeAnimais = quantidadeUnidadeSaida;

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();
           
            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

             await useCase.Execute(saidaAnimais.Id, request);

            saidaAnimais.DataSaida.ShouldBe(request.DataSaida);
            saidaAnimais.QuantidadeAnimais.ShouldBe(saidaAnimaisQuantidadeSaidaEsperada);
            unidadeOrigem.QuantidadeAnimais.ShouldBe(quantidadeUnidadeOrigemSaidaEsperada);
            unidadeSaida.QuantidadeAnimais.ShouldBe(quantidadeUnidadeSaidaSaidaEsperada);
        }

        [Fact]
        public async Task Sucess_Update_Quantities_from_UnidadeSaida_to_UnidadeOrigem()
        {
            MapsterTestSetup.Initialize();

            var quantidadeUnidadeOrigem = 5;
            var quantidadeUnidadeSaida = 7;
            var requestQuantidadeAnimais = 2;
            var saidaAnimaisQuantidade = 4;

            var quantidadeUnidadeOrigemSaidaEsperada = 7;
            var quantidadeUnidadeSaidaSaidaEsperada = 5;
            var saidaAnimaisQuantidadeSaidaEsperada = 2;


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();


            saidaAnimais.QuantidadeAnimais = saidaAnimaisQuantidade;
            request.QuantidadeAnimais = requestQuantidadeAnimais;
            unidadeOrigem.QuantidadeAnimais = quantidadeUnidadeOrigem;
            unidadeSaida.QuantidadeAnimais = quantidadeUnidadeSaida;


            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            await useCase.Execute(saidaAnimais.Id, request);


            saidaAnimais.DataSaida.ShouldBe(request.DataSaida);
            saidaAnimais.QuantidadeAnimais.ShouldBe(saidaAnimaisQuantidadeSaidaEsperada);
            unidadeOrigem.QuantidadeAnimais.ShouldBe(quantidadeUnidadeOrigemSaidaEsperada);
            unidadeSaida.QuantidadeAnimais.ShouldBe(quantidadeUnidadeSaidaSaidaEsperada);
        }

        [Fact]
        public async Task Sucess_Not_UpdateQuantities_When_Requested_Quantity_Is_Unchanged()
        {
            MapsterTestSetup.Initialize();


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();

            request.QuantidadeAnimais = saidaAnimais.QuantidadeAnimais;

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            await useCase.Execute(saidaAnimais.Id, request);

            true.ShouldBeTrue();
            saidaAnimais.DataSaida.ShouldBe(request.DataSaida);

        }

        [Fact]
        public async Task Error_Must_throw_exception_when_Saida_Animais_not_found()
        {
            MapsterTestSetup.Initialize();


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();

            request.QuantidadeAnimais = saidaAnimais.QuantidadeAnimais;

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder() .WithSaidaAnimaisInexist().Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id,request));
            exception.Message.ShouldBe(ResourceMessagesException.ANIMAL_EXIT_NOT_FOUND);

        }

        [Fact]
        public async Task Error_Must_throw_exception_when_UnidadeExploracao_Origem_not_found()
        {
            MapsterTestSetup.Initialize();


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();

            request.QuantidadeAnimais = saidaAnimais.QuantidadeAnimais;

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, null)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id, request));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

        }

        [Fact]
        public async Task Error_Must_throw_exception_when_UnidadeExploracao_Saida_not_found()
        {
            MapsterTestSetup.Initialize();


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();

            request.QuantidadeAnimais = saidaAnimais.QuantidadeAnimais;

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, null).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(saidaAnimais.Id, request));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);

        }

        [Fact]
        public async Task Error_Throw_When_Increasing_Quantity_And_UnidadeOrigem_Has_Insufficient_Animals()

        {
            MapsterTestSetup.Initialize();


            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();
            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();

            request.QuantidadeAnimais += saidaAnimais.QuantidadeAnimais;
            request.QuantidadeAnimais += unidadeOrigem.QuantidadeAnimais;

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

       

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            var exception = await Should.ThrowAsync<BusinessException>(() => useCase.Execute(saidaAnimais.Id,request));
            exception.Message.ShouldBe(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);
        }

        [Fact]
        public async Task Error_Throw_When_Decreasing_Quantity_And_Destination_Unit_Has_Insufficient_Animals()

        {
            MapsterTestSetup.Initialize();


            var quantidadeUnidadeOrigem = 5;
            var quantidadeUnidadeSaida = 7;
            var requestQuantidadeAnimais = 8;
            var saidaAnimaisQuantidade = 9;

            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();
            var saidaAnimais = SaidaAnimaisBuilder.Build();
            var request = RequestUpdateSaidaAnimaisJsonBuilder.Build();


            saidaAnimais.QuantidadeAnimais = saidaAnimaisQuantidade;
            request.QuantidadeAnimais = requestQuantidadeAnimais;
            unidadeOrigem.QuantidadeAnimais = quantidadeUnidadeOrigem;
            unidadeSaida.QuantidadeAnimais = quantidadeUnidadeSaida;

            unidadeOrigem.Id = saidaAnimais.UnidadeExploracaoOrigem.Id;
            unidadeSaida.Id = saidaAnimais.UnidadeExploracaoDestino.Id;

            var saidaAnimaisReadOnlyRepository = new SaidaAnimaisReadOnlyRepositoryBuilder()
                .WithSaidaAnimaisExist(saidaAnimais).Build();

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(unidadeOrigem.Id, unidadeOrigem)
                                                                                                             .WithUnidadeExploracaoById(unidadeSaida.Id, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository, saidaAnimaisReadOnlyRepository);

            var exception = await Should.ThrowAsync<BusinessException>(() => useCase.Execute(saidaAnimais.Id, request));
            exception.Message.ShouldBe(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);
        }

        private static UpdateSaidaAnimaisUseCase CreateUseCase(IUnidadeExploracaoReadOnlyRepository unidadeExploracaoReadRepository, ISaidaAnimaisReadOnlyRepository saidaAnimaisReadOnlyRepository)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var unidadeExploracaoUpdateRepository = UnidadeExploracaoUpdateOnlyRepository.Build();
            var saidaAnimaisUpdateOnlyRepository = SaidaAnimaisUpdateOnlyRepository.Build();

            return new UpdateSaidaAnimaisUseCase(saidaAnimaisUpdateOnlyRepository, unitOfWork, saidaAnimaisReadOnlyRepository, unidadeExploracaoReadRepository, unidadeExploracaoUpdateRepository);
                                                 
        }
    }
}
