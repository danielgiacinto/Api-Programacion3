using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Services.PersonaService.Queries
{
    public class GetCantidadMasculinoQuery : IRequest<string>
    {
    }
    public class GetCantidadMasculinoValidator : AbstractValidator<GetCantidadMasculinoQuery>
    {

    }
    public class GetCantidadMasculinoHandler : IRequestHandler<GetCantidadMasculinoQuery, string>
    {
        private readonly PersonasContext _context;

        public GetCantidadMasculinoHandler(PersonasContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(GetCantidadMasculinoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cantidadMasculino = await _context.Personas.Where(p => p.IdSexo == 2).CountAsync();

                if (cantidadMasculino > 0)
                {
                    return "Hay " + cantidadMasculino + " personas con sexo Masculino.";
                }
                else
                {
                    return "No se encuentran personas con sexo Masculino";
                }
            }
            catch (Exception)
            {
                throw new Exception("Error no se encuentran los datos");
            }

        }
    }
}
