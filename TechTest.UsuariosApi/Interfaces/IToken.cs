using Microsoft.AspNetCore.Identity;
using UsuariosApi.Models;

namespace UsuariosApi.Interfaces
{
    public interface IToken
    {
        public Token CreateToken(IdentityUser<int> usuario);
    }
}
