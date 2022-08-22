using FluentResults;
using UsuariosApi.Data.Requests;

namespace UsuariosApi.Interfaces
{
    public interface ILoginUser
    {
        public Result LogaUsuario(LoginRequest request);
    }
}
