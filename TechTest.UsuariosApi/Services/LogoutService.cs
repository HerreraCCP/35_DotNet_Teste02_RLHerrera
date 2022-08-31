using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public LogoutService(SignInManager<CustomIdentityUser> signInManager) => _signInManager = signInManager;

        public Result UserLogout() => _signInManager.SignOutAsync().IsCompletedSuccessfully ? Result.Ok() : Result.Fail("Deu ruim no Logout");
    }
}
