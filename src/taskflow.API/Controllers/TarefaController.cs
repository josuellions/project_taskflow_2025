using Microsoft.AspNetCore.Mvc;
using taskflow.API.Communication.Requests;
using taskflow.API.Communication.Responses;
using taskflow.API.Entities;
using taskflow.API.UseCases.Tasks.DeleteCurrent;
using taskflow.API.UseCases.Tasks.GetCurrent;
using taskflow.API.UseCases.Tasks.PostCurrent;
using taskflow.API.UseCases.Tasks.PutCurrent;

namespace taskflow.API.Controllers
{
    public class TarefaController : TaskFlowBaseController
    {
        [HttpGet]
        [Route("{taskId}")]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult GetCurrentId(
            [FromRoute] int taskId,
            GetCurrentTaskUseCase useCase
            )
        {
            var result = useCase.Execute(taskId);

            return Ok(result);
        }

        [HttpPost]
        [Route("{projectId}")]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)] 
        public ActionResult Create(
            [FromRoute] int projectId,
            [FromBody] RequestTaskJson request,
            [FromServices] PostCurrentTaskUseCase useCase
            )
        {
            var result = useCase.Execute(projectId, request);

            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public ActionResult Update(
          [FromRoute] int taskId,
          [FromBody] RequestTaskJson request,
          [FromServices] PutCurrentTaskUseCase useCase
          )
        {
            var result = useCase.Execute(taskId, request);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public ActionResult Delete(
        [FromRoute] int taskId,
        [FromBody] RequestTaskJson request,
        [FromServices] DeleteCurrentTaskUseCase useCase
        )
        {
            useCase.Execute(taskId, request);

            return Ok();
        }
    }
}
