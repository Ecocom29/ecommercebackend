using Ecommerce.Application.Features.Auths.Users.VMS;
using MediatR;

namespace Ecommerce.Application.Features.Auths.Users.Queries.GetUserByUserName
{
    public  class GetUserByUserNameQuery : IRequest<AuthResponse>
    {
        public string? UserName { get; set; }

        public GetUserByUserNameQuery(string username)
        {
            UserName = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
