using Api_2W1_CQRS.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api_2W1_CQRS.Business.UsuarioBusiness.Commands.LoginUsuario;

namespace Api_2W1_CQRS.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("usuario/login")]
        public async Task<bool> LoginUsuario([FromBody] LoginUsuarioCommand comando)
        {
            return await _mediator.Send(comando);
        }

    }
}
