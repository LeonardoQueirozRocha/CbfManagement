using Cbf.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Cbf.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/jogadores")]
    public class JogadoresController : MainController
    {
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok();
        }
    }
}
