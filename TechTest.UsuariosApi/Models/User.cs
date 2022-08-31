using System;

namespace UsuariosApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }
        
        public string Email { get; set; }

        public DateTime Birthday { get; set; }
    }
}