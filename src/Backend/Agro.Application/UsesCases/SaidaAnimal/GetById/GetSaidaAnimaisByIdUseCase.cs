using Agro.Communication.Response;
using Agro.Domain.Repositories.SaidaAnimal;
using Agro.Exceptions;
using Agro.Exceptions.ExceptionsBase;
using Mapster;

namespace Agro.Application.UsesCases.SaidaAnimal.GetById
{
    public class GetSaidaAnimaisByIdUseCase(
            ISaidaAnimaisReadOnlyRepository readOnlyRepository) : IGetSaidaAnimaisByIdUseCase
    {
        private readonly ISaidaAnimaisReadOnlyRepository _readOnlyRepository = readOnlyRepository;

        public async Task<ResponseSaidaAnimaisJson> Execute(int saidaAnimaisId)
        {
            var saidaAnimais = await _readOnlyRepository.GetById(saidaAnimaisId);

            if (saidaAnimais is null)
                throw new NotFoundException(ResourceMessagesException.ANIMAL_EXIT_NOT_FOUND);
            
            return saidaAnimais.Adapt<ResponseSaidaAnimaisJson>();
        }
    }
}


