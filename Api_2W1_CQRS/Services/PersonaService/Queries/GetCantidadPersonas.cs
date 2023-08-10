using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Services.PersonaService.Queries
{
    public class GetCantidadPersonasQuery : IRequest<string>
    {

    }
    public class GetCantidadPersonasValidator : AbstractValidator<GetCantidadPersonasQuery>
    {

    }

    public class GetCantidadPersonasHandler : IRequestHandler<GetCantidadPersonasQuery, string>
    {
        private readonly PersonasContext _context;

        public GetCantidadPersonasHandler(PersonasContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetCantidadPersonasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cantidadPersonas = await _context.Personas.CountAsync();

                if (cantidadPersonas > 0)
                {
                    return "Se encuentran " + cantidadPersonas + " personas en el sistema";
                }
                else
                {
                    return "No se encuentran personas en el sistema";
                }
            }
            catch (Exception)
            {
                throw new Exception("Error en la busqueda de personas");
            }
        }
    }
}
