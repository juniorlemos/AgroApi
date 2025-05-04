using Agro.Domain.Repositories.Especie;
using Agro.Application.UsesCases.UnidadeDeExploracao.Register;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using Shouldly;

namespace UseCases.Test.UnidadeExploracaoTest.Register
{
    public class RegisterUnidadeExploracaoUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            MapsterTestSetup.Initialize();

            var request = RequestRegisterUnidadeExploracaoJsonBuilder.Build();

            var readRepository = new EspecieReadOnlyRepositoryBuilder().WithEspecieExist().Build();

            var useCase = CreateUseCase(readRepository);

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.CodigodePropriedade.ShouldBe(request.CodigoPropriedade);
            result.QuantidadeAnimais.ShouldBe(request.QuantidadeAnimais);
            result.CodigoEspecie.ShouldBe(request.CodigoEspecie);
        }


        [Fact]
        public async Task Error_Must_throw_exception_when_species_does_not_exist()
        {
            var request = RequestRegisterUnidadeExploracaoJsonBuilder.Build();

            var readRepository = new EspecieReadOnlyRepositoryBuilder().WithEspecieInexist().Build();

            var useCase = CreateUseCase(readRepository);

            var exception = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(request));
            exception.Message.ShouldBe(ResourceMessagesException.THE_SPECIES_DOES_NOT_EXIST);
        }

        private static RegisterUnidadeExploracaoUseCase CreateUseCase(IEspecieReadOnlyRepository readRepository)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var writeRepository = UnidadeExploracaoWriteOnlyRepositoryBuilder.Build();

            return new RegisterUnidadeExploracaoUseCase(writeRepository, readRepository, unitOfWork);
        }
    }
}
