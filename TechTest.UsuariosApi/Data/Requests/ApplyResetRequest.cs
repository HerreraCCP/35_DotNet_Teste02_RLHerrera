using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class ApplyResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
