using Agro.Communication.Request;
using Agro.Communication.Response;
using Agro.Domain.Entities;
using Agro.Domain.Extensions;
using Agro.Domain.Repositories;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Domain.Repositories.UnidadeDeExploracao;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using Mapster;

namespace Agro.Application.UsesCases.SaidaAnimal.Register
{
    public class RegisterSaidaAnimaisUseCase(
         ISaidaAnimaisWriteOnlyRepository writeOnlyRepository,
         IUnitOfWork unitOfWork,
         IUnidadeExploracaoReadOnlyRepository readOnlyRepository,
         IUnidadeExploracaoUpdateOnlyRepository unidadeExploracaoUpdateOnlyRepository) : IRegisterSaidaAnimaisUseCase
    {
        private readonly ISaidaAnimaisWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUnidadeExploracaoReadOnlyRepository _readOnlyRepository = readOnlyRepository;
        private readonly IUnidadeExploracaoUpdateOnlyRepository _unidadeExploracaoUpdateOnlyRepository = unidadeExploracaoUpdateOnlyRepository;

        public async Task<ResponseRegisterSaidaAnimaisJson> Execute(RequestRegisterSaidaAnimaisJson request)
        {
            
            await Validate(request);

            if (request.CodigoUEPOrigem == request.CodigoUEPSaida)
                throw new BusinessException(ResourceMessagesException.ORIGIN_UEP_CANNOT_BE_DESTINATION_UEP);

            var unidadeExploracaoOrigem = await _readOnlyRepository.GetById(request.CodigoUEPOrigem);

            if (unidadeExploracaoOrigem is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
            
            var unidadeExploracaoSaida = await _readOnlyRepository.GetById(request.CodigoUEPSaida);

            if (unidadeExploracaoSaida is null)
                throw new NotFoundException(ResourceMessagesException.EXPLORATION_UNIT_NOT_FOUND);
            
            if (request.QuantidadeAnimais > unidadeExploracaoOrigem.QuantidadeAnimais!)
                throw new BusinessException(ResourceMessagesException.QUANTITY_EXCEEDS_AVAILABLE_SOURCE_ANIMALS);

            var saidaAnimais = request.Adapt<SaidaAnimais>();

            unidadeExploracaoOrigem.QuantidadeAnimais -= saidaAnimais.QuantidadeAnimais;
            unidadeExploracaoSaida.QuantidadeAnimais += saidaAnimais.QuantidadeAnimais;

            await _writeOnlyRepository.Add(saidaAnimais);

            _unidadeExploracaoUpdateOnlyRepository.Update(unidadeExploracaoOrigem);

             _unidadeExploracaoUpdateOnlyRepository.Update(unidadeExploracaoSaida);

            await _unitOfWork.Commit();

            return new ResponseRegisterSaidaAnimaisJson
            {
                DataSaida = saidaAnimais.DataSaida,
                QuantidadeAnimais = saidaAnimais.QuantidadeAnimais,
                CodigoUEPOrigem = saidaAnimais.CodigoUEPOrigem,
                CodigoUEPSaida = saidaAnimais.CodigoUEPSaida
            };
        }
        private static async Task Validate(RequestRegisterSaidaAnimaisJson request)
        {
            var validator = new RegisterSaidaAnimaisValidator();
            var result = await validator.ValidateAsync(request);

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}