using Api_2W1_CQRS.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api_2W1_CQRS.Business.UsuarioBusiness.Commands
{
    public class LoginUsuario
    {
        public class LoginUsuarioCommand : IRequest<bool>
        {
            public string Usuario1 { get; set; }

            public string Password { get; set; }

        }
        public class LoginUsuarioValidator : AbstractValidator<LoginUsuarioCommand>
        {
            public LoginUsuarioValidator() 
            {
                RuleFor(u => u.Usuario1).NotEmpty();
                RuleFor(u => u.Password).NotEmpty();
            }

        }

        public class LoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, bool>
        {

            private readonly PersonasContext _context;
            private readonly LoginUsuarioValidator _validator;

            public LoginUsuarioHandler(PersonasContext context, LoginUsuarioValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<bool> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                try
                {
                    var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario1 == request.Usuario1 && u.Password == request.Password);
                    if(usuario != null)
                    {
                        return true;
                    }
                    else 
                    { 
                        return false; 
                    
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
