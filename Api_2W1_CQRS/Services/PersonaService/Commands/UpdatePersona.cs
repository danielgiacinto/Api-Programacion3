using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Services.PersonaBusiness.Commands
{
    public class UpdatePersona
    {
        public class UpdatePersonaCommand : IRequest<Persona>
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public int Dni { get; set; }

            public int IdSexo { get; set; }

            public int IdPais { get; set; }

            public int IdProvincia { get; set; }
        }

        public class PostPersonaValidation : AbstractValidator<UpdatePersonaCommand>
        {
            public PostPersonaValidation()
            {
                RuleFor(p => p.Id).NotEmpty();
                RuleFor(p => p.Nombre).NotEmpty();
                RuleFor(p => p.Apellido).NotEmpty();
                RuleFor(p => p.Dni).NotEmpty();
                RuleFor(p => p.IdSexo).NotEmpty();
                RuleFor(p => p.IdPais).NotEmpty();
                RuleFor(p => p.IdProvincia).NotEmpty();
            }
        }

        public class PostPersonaHandler : IRequestHandler<UpdatePersonaCommand, Persona>
        {

            private readonly PersonasContext _context;
            private readonly PostPersonaValidation _validation;
            public PostPersonaHandler(PersonasContext context, PostPersonaValidation validation)
            {
                _context = context;
                _validation = validation;
            }

            public async Task<Persona> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
            {
                _validation.Validate(request);
                
                try
                {
                    var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Id == request.Id);
                    if(persona !=  null)
                    {
                        persona.Nombre = request.Nombre;
                        persona.Apellido = request.Apellido;
                        persona.Dni = request.Dni;
                        persona.IdSexo = request.IdSexo;
                        persona.IdPais = request.IdPais;
                        persona.IdProvincia = request.IdProvincia;

                        await _context.SaveChangesAsync();

                        return persona;

                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(persona));
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
