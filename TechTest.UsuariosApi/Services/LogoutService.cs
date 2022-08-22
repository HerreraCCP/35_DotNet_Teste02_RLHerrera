using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UsuariosApi.Interfaces;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager) => _signInManager = signInManager;

        public Result UserLogout() => _signInManager.SignOutAsync().IsCompletedSuccessfully ? Result.Ok() : Result.Fail("Deu ruim no Logout");
    }
}
