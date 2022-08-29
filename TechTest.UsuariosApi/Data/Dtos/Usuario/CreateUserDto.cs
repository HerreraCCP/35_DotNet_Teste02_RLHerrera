using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos.Usuario
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}