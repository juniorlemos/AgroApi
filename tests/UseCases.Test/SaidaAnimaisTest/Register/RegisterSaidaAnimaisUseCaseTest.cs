using Agro.Application.UsesCases.SaidaAnimal.Register;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using Shouldly;

namespace UseCases.Test.SaidaAnimaisTest.Register
{
    public class RegisterSaidaAnimaisUseCaseTest
    {
            [Fact]
            public async Task Sucess()
            {
                MapsterTestSetup.Initialize();

                var unidadeOrigem = UnidadeExploracaoBuilder.Build();
                var unidadeSaida = UnidadeExploracaoBuilder.Build();

                unidadeSaida.Id += unidadeOrigem.Id; 


                var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();

                request.CodigoUEPOrigem = unidadeOrigem.Id;
                request.CodigoUEPSaida = unidadeSaida.Id;

                unidadeOrigem.QuantidadeAnimais += request.QuantidadeAnimais;

                var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(request.CodigoUEPOrigem, unidadeOrigem)
                                                                                                              .WithUnidadeExploracaoById(request.CodigoUEPSaida, unidadeSaida).Build();
                var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository);

                var result = await useCase.Execute(request);

                result.ShouldNotBeNull();
                result.DataSaida.ShouldBe(request.DataSaida);
                result.QuantidadeAnimais.ShouldBe(request.QuantidadeAnimais);
                result.CodigoUEPOrigem.ShouldBe(request.CodigoUEPOrigem);
                result.CodigoUEPSaida.ShouldBe(request.CodigoUEPSaida);
            }



        [Fact]
        public async Task Error_Throw_When_Origin_UnidadeExploration_be_Destination_UnidadeExploration()
        {
            MapsterTestSetup.Initialize();

            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();

            unidadeSaida.Id += unidadeOrigem.Id;


            var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();

            request.CodigoUEPOrigem = request.CodigoUEPSaida;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(request.CodigoUEPOrigem, unidadeOrigem)
                                                                                                           .WithUnidadeExploracaoById(request.CodigoUEPSaida, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<BusinessException>(() => useCase.Execute(request));
            exception.Message.ShouldBe(ResourceMessagesException.ORIGIN_UEP_CANNOT_BE_DESTINATION_UEP);
        }

        [Fact]
        public async Task Error_Must_throw_exception_when_UnidadeExploracao_Origem_not_found()
        {
            MapsterTestSetup.Initialize();

            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();

            unidadeSaida.Id += unidadeOrigem.Id;


            var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();

            request.CodigoUEPOrigem = unidadeOrigem.Id;
            request.CodigoUEPSaida = unidadeSaida.Id;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(request.CodigoUEPOrigem, null)
                                                                                                           .WithUnidadeExploracaoById(request.CodigoUEPSaida, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(request));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Must_throw_exception_when_UnidadeExploracao_Origem_not_Saida()
        {
            MapsterTestSetup.Initialize();

            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();

            unidadeSaida.Id += unidadeOrigem.Id;


            var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();

            request.CodigoUEPOrigem = unidadeOrigem.Id;
            request.CodigoUEPSaida = unidadeSaida.Id;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(request.CodigoUEPOrigem, unidadeOrigem)
                                                                                                           .WithUnidadeExploracaoById(request.CodigoUEPSaida, null).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(request));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }

        [Fact]
        public async Task Error_Throw_When_Quantity_Exceeds_Available_Animals_In_UnidadeExploracao()
        {
            MapsterTestSetup.Initialize();

            var unidadeOrigem = UnidadeExploracaoBuilder.Build();
            var unidadeSaida = UnidadeExploracaoBuilder.Build();

            unidadeSaida.Id += unidadeOrigem.Id;


            var request = RequestRegisterSaidaAnimaisJsonBuilder.Build();


            request.CodigoUEPOrigem = unidadeOrigem.Id;
            
            request.CodigoUEPSaida = unidadeSaida.Id;

            request.QuantidadeAnimais += unidadeOrigem.QuantidadeAnimais;

            var unidadeExplorationReadOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoById(request.CodigoUEPOrigem, unidadeOrigem)
                                                                                                           .WithUnidadeExploracaoById(request.CodigoUEPSaida, unidadeSaida).Build();
            var useCase = CreateUseCase(unidadeExplorationReadOnlyRepository);

            var exception = await Should.ThrowAsync<BusinessException>(() => useCase.Execute(request));
            exception.Message.ShouldBe(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);
        }

          private static RegisterSaidaAnimaisUseCase CreateUseCase(IUnidadeExploracaoReadOnlyRepository readRepository)
            {
                var unitOfWork = UnitOfWorkBuilder.Build();
                var writeRepository = SaidaAnimaisWriteOnlyRepositoryBuilder.Build();
                var updateRepository = UnidadeExploracaoUpdateOnlyRepository.Build();


            return new RegisterSaidaAnimaisUseCase(writeRepository, unitOfWork, readRepository, updateRepository);
            }

        }
    
}
