using Microsoft.AspNetCore.Mvc;
using taskflow.API.Communication.Requests;
using taskflow.API.Communication.Responses;
using taskflow.API.UseCases.Report.GetCurrent;

namespace taskflow.API.Controllers
{
    public class RelatorioController : TaskFlowBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(IList<ResponseReportJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrent([FromBody] RequestReportJson request, GetCurrentReportUseCase useCase) 
        {

            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}
