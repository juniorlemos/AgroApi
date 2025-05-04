using Agro.Application.UsesCases.SaidaAnimal.Delete;
using Agro.Application.UsesCases.SaidaAnimal.GetAll;
using Agro.Application.UsesCases.SaidaAnimal.GetById;
using Agro.Application.UsesCases.SaidaAnimal.Register;
using Agro.Application.UsesCases.SaidaAnimal.Update;
using Agro.Communication.Request;
using Agro.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace Agro.API.Controllers
{ 
    public class SaidaAnimaisController : AgroApiBaseController
    {    
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseSaidaAnimaisJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
               [FromServices] IGetSaidaAnimaisByIdUseCase useCase,
               [FromRoute] int id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseSaidaAnimaisJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
           [FromServices] IGetAllSaidaAnimaisUseCase useCase,
           [FromQuery] int page = 0,
           [FromQuery] int pageSize = 0)
        {
            var response = await useCase.Execute(page, pageSize);

            if (response.Any())
                return Ok(response);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseSaidaAnimaisJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
           [FromServices] IRegisterSaidaAnimaisUseCase useCase,
           [FromBody] RequestRegisterSaidaAnimaisJson request)
        {  
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
        [FromServices] IUpdateSaidaAnimaisUseCase useCase,
        [FromRoute]int id,
        [FromBody] RequestUpdateSaidaAnimaisJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
           [FromServices] IDeleteSaidaAnimaisUseCase useCase,
           [FromRoute] int id)
        {
            await useCase.Execute(id);

            return NoContent();
        }
    }
}