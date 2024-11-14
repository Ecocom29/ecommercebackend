using AutoMapper;
using Ecommerce.Application.Features.Adresses.VMS;
using Ecommerce.Application.Features.Auths.Users.VMS;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, AuthResponse>
    {

        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWord;
        private readonly IMapper _mapper;

        public GetUserByTokenQueryHandler(
            UserManager<Usuario> userManager, 
            IAuthService authService, 
            IUnitOfWork unitOfWord, 
            IMapper mapper)
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWord = unitOfWord;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());

            if (user is null)
            {
                throw new Exception("El usuario no está autenticado.");
            }

            if (!user.IsActive)
            {
                throw new Exception("El usuario está bloqueado.");
            }

            var direccionEnvio = await _unitOfWord.Repository<Address>().GetEntityAsync(
                x=>x.Username == user.UserName
            );

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Telefono = user.Telefono,
                Email = user.Email,
                UserName = user.UserName,
                Avatar = user.AvatarUrl,
                DireccionEnvio = _mapper.Map<AddressVM>(direccionEnvio),
                Token = _authService.CreateToken(user, roles),
                Roles = roles
            };
        }
    }
}
