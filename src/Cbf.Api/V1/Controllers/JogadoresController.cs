using Cbf.Api.Controllers;
using Cbf.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cbf.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/jogadores")]
    public class JogadoresController : MainController
    {
        public JogadoresController(INotificador notificador) : base(notificador) { }

        [HttpGet]
        public IActionResult ObterTodos() => Ok();
    }
}
