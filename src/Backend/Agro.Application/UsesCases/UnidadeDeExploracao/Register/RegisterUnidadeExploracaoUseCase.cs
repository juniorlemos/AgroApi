using Agro.Communication.Request;
using Agro.Communication.Response;
using Agro.Domain.Entities;
using Agro.Domain.Extensions;
using Agro.Domain.Repositories;
using Agro.Domain.Repositories.Especie;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using Mapster;

namespace Agro.Application.UsesCases.UnidadeDeExploracao.Register
{
    public class RegisterUnidadeExploracaoUseCase(
        IUnidadeExploracaoWriteOnlyRepository writeOnlyRepository,
        IEspecieReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork) : IRegisterUnidadeExploracaoUseCase
    {
        private readonly IUnidadeExploracaoWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
        private readonly IEspecieReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseRegisterUnidadeExploracaoJson> Execute(RequestRegisterUnidadeExploracaoJson request)
        {
            await Validate(request);

            var existEspecie = await _readOnlyRepository.ExistEspecie(request.CodigoEspecie);

            if (existEspecie.IsFalse())
                throw new NotFoundException(ResourceMessagesException.THE_SPECIES_DOES_NOT_EXIST);           

            var unidadeExploracao = request.Adapt<UnidadeExploracao>();       

            await _writeOnlyRepository.Add(unidadeExploracao);
            await _unitOfWork.Commit();

            return new ResponseRegisterUnidadeExploracaoJson
            {
                QuantidadeAnimais = unidadeExploracao.QuantidadeAnimais,
                CodigodePropriedade = unidadeExploracao.CodigoPropriedade,
                CodigoEspecie = unidadeExploracao.EspecieId,             
            };           
        }

        private static async Task Validate(RequestRegisterUnidadeExploracaoJson request)
        {
            var validator = new RegisterUnidadeExploracaoValidator();
            var result = await validator.ValidateAsync(request);

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
