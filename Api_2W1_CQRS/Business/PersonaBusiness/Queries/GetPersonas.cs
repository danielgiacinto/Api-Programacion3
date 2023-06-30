using Api_2W1_CQRS.Models;
using Api_2W1_CQRS.Resultados;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api_2W1_CQRS.Business.PersonaBusiness.Queries
{
    public class GetPersonas
    {
        public class GetPersonasQuery : IRequest <List<ResultadoPersona>>
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public string Apellido { get; set; }

            public int Dni { get; set; }

            public int IdSexo { get; set; }

            public int IdPais { get; set; }

            public int IdProvincia { get; set; }
        }

        public class GetPersonasValidation : AbstractValidator<GetPersonasQuery>
        {
            public GetPersonasValidation()
            {
                RuleFor(p => p.Id).NotEmpty();
                RuleFor(p =>p.Nombre).NotEmpty();
                RuleFor(p => p.Apellido).NotEmpty();
                RuleFor(p => p.Dni).NotEmpty();
                RuleFor(p => p.IdSexo).NotEmpty();
                RuleFor(p =>p.IdPais).NotEmpty();
                RuleFor(p => p.IdProvincia).NotEmpty();
            }
        }

        public class GetPersonasHandler : IRequestHandler<GetPersonasQuery, List<ResultadoPersona>>
        {
            
            private readonly PersonasContext _context;
            private readonly GetPersonasValidation _validation;

            public GetPersonasHandler(PersonasContext context, GetPersonasValidation validation)
            {
                _context = context;
                _validation = validation;
            }

            public async Task<List<ResultadoPersona>> Handle(GetPersonasQuery request, CancellationToken cancellationToken)
            {
                _validation.Validate(request);

                var result = new List<ResultadoPersona>();
                var personas = await _context.Personas
                    .Include(p => p.IdPaisNavigation)
                    .Include(p => p.IdSexoNavigation)
                    .Include(p => p.IdProvinciaNavigation)
                    .ToListAsync();

                if (personas.Count != 0)
                {
                    foreach (var p in personas)
                    {
                        var itemPersona = new ResultadoPersona
                        {
                            Id = p.Id,
                            Nombre = p.Nombre,
                            Apellido = p.Apellido,
                            Dni = p.Dni,
                            Sexo1 = p.IdSexoNavigation.Sexo1,
                            Pais = p.IdPaisNavigation.Pais,
                            Provincia = p.IdProvinciaNavigation.Provincia,
                        };
                        
                        result.Add(itemPersona);
                    }
                    return result;
                }
                
                return result;
            }

        }
    }
}
