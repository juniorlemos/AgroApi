using Agro.Application.UsesCases.UnidadeDeExploracao.Delete;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetAll;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetQuantidadeAnimaisByIdUseCase;
using Agro.Application.UsesCases.UnidadeDeExploracao.GetById.GetUnidadeExploracaoByIdUseCase;
using Agro.Application.UsesCases.UnidadeDeExploracao.Register;
using Agro.Communication.Request;
using Agro.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Agro.API.Controllers
{
   
    public class UnidadeExploracaoController : AgroApiBaseController
    {
        [HttpGet]
        [Route("{id}/quantidade-animais")]
        [ProducesResponseType(typeof(ResponseUnidadeExploracaoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdQuantidadeAnimais(
                [FromServices] IGetQuantidadeAnimaisByIdUnidadeUseCase useCase,
                [FromRoute] int id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseUnidadeExploracaoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdUnidadeExploracao(
               [FromServices] IGetUnidadeExploracaoByIdUseCase useCase,
               [FromRoute] int id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUnidadeExploracaoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllUnidadeExploracaoUseCase useCase,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 0)
        {
            var response = await useCase.Execute(page, pageSize);

            if (response.Any())
                return Ok(response);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUnidadeExploracaoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
        [FromServices] IRegisterUnidadeExploracaoUseCase useCase,
        [FromBody] RequestRegisterUnidadeExploracaoJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
        [FromServices] IDeleteUnidadeExploracaoUseCase useCase,
        [FromRoute] int id)
        {
            await useCase.Execute(id);

            return NoContent();
        }

    }
}