using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
   public class Login
    {
            public int ID { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }
}
