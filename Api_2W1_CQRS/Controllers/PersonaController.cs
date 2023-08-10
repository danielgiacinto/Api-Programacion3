using Api_2W1_CQRS.Business.PersonaBusiness.Commands;
using Api_2W1_CQRS.Business.PersonaBusiness.Queries;
using Api_2W1_CQRS.Models;
using Api_2W1_CQRS.Resultados;
using Api_2W1_CQRS.Services.PersonaBusiness.Commands;
using Api_2W1_CQRS.Services.PersonaService.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api_2W1_CQRS.Business.PersonaBusiness.Commands.DeletePersona;
using static Api_2W1_CQRS.Business.PersonaBusiness.Commands.PostPersona;
using static Api_2W1_CQRS.Business.PersonaBusiness.Queries.GetPersonaByDocumento;
using static Api_2W1_CQRS.Business.PersonaBusiness.Queries.GetPersonas;
using static Api_2W1_CQRS.Services.PersonaBusiness.Commands.UpdatePersona;

namespace Api_2W1_CQRS.Controllers
{
    [ApiController]
    public class PersonaController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("persona/crear")]
        public async Task<Persona> CrearPersona([FromBody] PostPersonaCommand comando)
        {
            return await _mediator.Send(comando);
        }

        [HttpGet]
        [Route("persona/documento/{documento}")]
        public async Task<Persona> ObtenerPersonaDocumento(int documento)
        {
            return await _mediator.Send(new GetPersonaByDocumentoQuery { Documento = documento });
        }

        [HttpGet]
        [Route("personas/todos")]
        public async Task<List<ResultadoPersona>> ObtenerTodos()
        {
            return await _mediator.Send(new GetPersonasQuery());
        }

        [HttpDelete]
        [Route("personas/borrar/documento/{documento}")]
        public async Task<bool> BorrarPersonaDni(int documento)
        {
            return await _mediator.Send(new DeletePersonaCommand { Dni = documento});
        }

        [HttpPut]
        [Route("personas/actualizar")]
        public async Task<Persona> ActualizarPersona([FromBody] UpdatePersonaCommand comando)
        {
            return await _mediator.Send(comando);
        }

        [HttpGet]
        [Route("personas/cantidad")]
        public async Task<string> CantidadTotalPersonas()
        {
            return await _mediator.Send(new GetCantidadPersonasQuery());
        }
        [HttpGet]
        [Route("personas/masculino")]
        public async Task<string> CantidadTotalMasculino()
        {
            return await _mediator.Send(new GetCantidadMasculinoQuery());
        }
    }
}
