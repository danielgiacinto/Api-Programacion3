using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Business.PersonaBusiness.Commands
{
    public class DeletePersona
    {
        public class DeletePersonaCommand : IRequest<bool>
        {
            public int Dni { get; set; }
        }

        public class DeletePersonaValidation : AbstractValidator<DeletePersonaCommand>
        {
            public DeletePersonaValidation()
            {
                RuleFor(d => d.Dni).NotEmpty();
            }
        }

        public class DeletePersonaHandler : IRequestHandler<DeletePersonaCommand, bool>
        {
            private readonly PersonasContext _context;
            private readonly DeletePersonaValidation _validation;

            public DeletePersonaHandler(PersonasContext context, DeletePersonaValidation validation)
            {
                _context = context;
                _validation = validation;
            }
            public async Task<bool> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
            {
                
                _validation.Validate(request);

                var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Dni == request.Dni);

                if(persona != null) 
                {
                    _context.Personas.Remove(persona);
                    await _context.SaveChangesAsync();
                    
                    return true;
                }

                return false;
            }
        }
    }
}
