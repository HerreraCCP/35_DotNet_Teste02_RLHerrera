using Microsoft.AspNetCore.Identity;
using UsuariosApi.Models;

namespace UsuariosApi.Interfaces
{
    public interface IToken
    {
        public Token CreateToken(CustomIdentityUser usuario);
    }
}
