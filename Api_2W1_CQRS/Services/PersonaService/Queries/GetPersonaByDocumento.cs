using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Business.PersonaBusiness.Queries
{
    public class GetPersonaByDocumento
    {
        public class GetPersonaByDocumentoQuery : IRequest<Persona>
        {
            public int Documento { get; set; }
        }

        public class GetPersonaByDocumentoValidation : AbstractValidator<GetPersonaByDocumentoQuery>
        {
            public GetPersonaByDocumentoValidation()
            {
                RuleFor(p => p.Documento).NotEmpty();
            }
        }

        public class GetPersonaByDocumentoHandler : IRequestHandler<GetPersonaByDocumentoQuery, Persona>
        {

            private readonly PersonasContext _context;
            private readonly GetPersonaByDocumentoValidation _validation;
            public GetPersonaByDocumentoHandler(PersonasContext context, GetPersonaByDocumentoValidation validation)
            {
                _context = context;
                _validation = validation;
            }
            public async Task<Persona> Handle(GetPersonaByDocumentoQuery request, CancellationToken cancellationToken)
            {
                
                _validation.Validate(request);

                try
                {
                    var persona =  await _context.Personas.FirstOrDefaultAsync(p => p.Dni == request.Documento);
                    if(persona != null) 
                    {
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
