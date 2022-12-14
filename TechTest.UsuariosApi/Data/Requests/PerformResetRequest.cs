using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class PerformResetRequest
    {
        [Required] 
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required] public string Token { get; set; }
    }
}