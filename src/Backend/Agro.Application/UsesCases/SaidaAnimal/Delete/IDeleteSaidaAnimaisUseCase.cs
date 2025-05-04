namespace Agro.Application.UsesCases.SaidaAnimal.Delete
{
    public interface IDeleteSaidaAnimaisUseCase
    {
        Task Execute(int unidadeExplorationId);
    }
}