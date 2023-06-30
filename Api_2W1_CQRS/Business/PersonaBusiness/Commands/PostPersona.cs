using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Business.PersonaBusiness.Commands
{
    public class PostPersona
    {
        public class PostPersonaCommand : IRequest<Persona>
        {
            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public int Dni { get; set; }

            public int IdSexo { get; set; }

            public int IdPais { get; set; }

            public int IdProvincia { get; set; }
        }

        public class PostPersonaValidation : AbstractValidator<PostPersonaCommand>
        {
            public PostPersonaValidation()
            {
                RuleFor(p => p.Nombre).NotEmpty();
                RuleFor(p => p.Apellido).NotEmpty();
                RuleFor(p => p.Dni).NotEmpty();
                RuleFor(p => p.IdSexo).NotEmpty();
                RuleFor(p => p.IdPais).NotEmpty();
                RuleFor(p => p.IdProvincia).NotEmpty();
            }
        }

        public class PostPersonaHandler : IRequestHandler<PostPersonaCommand, Persona>
        {

            private readonly PersonasContext _context;
            private readonly PostPersonaValidation _validation;
            public PostPersonaHandler(PersonasContext context, PostPersonaValidation validation)
            {
                _context = context;
                _validation = validation;
            }
            public async Task<Persona> Handle(PostPersonaCommand request, CancellationToken cancellationToken)
            {
                _validation.Validate(request);

                try
                {
                    var existe = await _context.Personas.FirstOrDefaultAsync(p => p.Dni == request.Dni);
                    if(existe != null)
                    {
                        throw new Exception("Ya existe una Persona con este DNI.");
                    }

                    Persona persona = new Persona();
                    persona.Nombre = request.Nombre;
                    persona.Apellido = request.Apellido;
                    persona.Dni = request.Dni;
                    persona.IdSexo = request.IdSexo;
                    persona.IdPais = request.IdPais;
                    persona.IdProvincia = request.IdProvincia;

                    await _context.Personas.AddAsync(persona);
                    await _context.SaveChangesAsync();

                    return persona;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
