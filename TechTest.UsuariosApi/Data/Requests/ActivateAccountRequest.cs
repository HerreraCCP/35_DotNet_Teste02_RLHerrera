using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class ActivateAccountRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ActivateCode { get; set; }
    }
}