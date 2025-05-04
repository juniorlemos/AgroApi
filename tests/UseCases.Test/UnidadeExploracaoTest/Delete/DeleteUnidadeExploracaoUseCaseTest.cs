using Agro.Application.UsesCases.UnidadeDeExploracao.Delete;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCases.Test.UnidadeExploracaoTest.Delete
{
    public class DeleteUnidadeExploracaoUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            MapsterTestSetup.Initialize();

            var unidade = UnidadeExploracaoBuilder.Build();

            unidade.QuantidadeAnimais = 0;

            var deleteOnlyRepository = new UnidadeExploracaoDeleteOnlyRepositoryBuilder().WithUnidadeExploracaoExist().Build();
            var readOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoExist(unidade).Build();

            var useCase = CreateUseCase(deleteOnlyRepository, readOnlyRepository);

            await useCase.Execute(unidade.Id);

            true.ShouldBeTrue();
        }


        [Fact]
        public async Task Error_Must_throw_exception_when_Exeploration_Unit_not_found()
        {
            var unidade = UnidadeExploracaoBuilder.Build();
            unidade.QuantidadeAnimais = 0;

            var deleteOnlyRepository = new UnidadeExploracaoDeleteOnlyRepositoryBuilder().WithUnidadeExploracaoExist().Build();
            var readOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoInexist().Build();

            var useCase = CreateUseCase(deleteOnlyRepository, readOnlyRepository);


            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(unidade.Id));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
        }


        [Fact]
        public async Task Error_Should_Throw_When_Unidade_Has_Animals()
        {
            var unidade = UnidadeExploracaoBuilder.Build();
            unidade.QuantidadeAnimais = 1;

            var deleteOnlyRepository = new UnidadeExploracaoDeleteOnlyRepositoryBuilder().WithUnidadeExploracaoExist().Build();
            var readOnlyRepository = new UnidadeExploracaoReadOnlyRepositoryBuilder().WithUnidadeExploracaoExist(unidade).Build();

            var useCase = CreateUseCase(deleteOnlyRepository, readOnlyRepository);


            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(unidade.Id));
            exception.Message.ShouldBe(ResourceMessagesException.EXPLORATION_UNIT_CANNOT_BE_DELETED_WITH_ANIMALS);
        }


        private static DeleteUnidadeExploracaoUseCase CreateUseCase(
            IUnidadeExploracaoDeleteOnlyRepository deleteOnlyRepository,
            IUnidadeExploracaoReadOnlyRepository readOnlyRepository)                               
        {
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteUnidadeExploracaoUseCase(deleteOnlyRepository, readOnlyRepository, unitOfWork);
        }
    }
}
