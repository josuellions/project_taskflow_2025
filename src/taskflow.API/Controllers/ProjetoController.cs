using Microsoft.AspNetCore.Mvc;
using taskflow.API.Communication.Requests;
using taskflow.API.Communication.Responses;
using taskflow.API.Entities;
using taskflow.API.UseCases.Projects.DeleteCurrent;
using taskflow.API.UseCases.Projects.GetCurrent;
using taskflow.API.UseCases.Projects.PostCurrent;
using taskflow.API.UseCases.Projects.PutCurrent;

namespace taskflow.API.Controllers
{
    public class ProjetoController : TaskFlowBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCurrent([FromServices] GetCurrentProjectUseCase useCase)
        {
            var result = useCase.Execute();

            return Ok(result);
        }

        [HttpGet]
        [Route("{projectId}")]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetCurrentId([FromRoute] int projectId, [FromServices] GetCurrentProjectUseCase useCase)
        {
            var result = useCase.GetCurrentId(projectId);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Project), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Create([FromBody] RequestProjectJson request, [FromServices] PostCurrentProjectUseCase useCase)
        {
            var result = useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] int projectId, [FromBody] RequestProjectJson request, [FromServices] PutCurrentProjectUseCase useCase)
        {
            var result = useCase.Execute(projectId, request);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int projectId, [FromBody] RequestProjectJson request, [FromServices] DeleteCurrentProjectUseCase useCase)
        {
            useCase.Execute(projectId, request);

            return Ok();
        }
    }
}
