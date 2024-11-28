using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
   public  class Player
    {
        public int ID { get; set; } 
        public string? Name { get; set; }
        public int LoginID {  get; set; }
        public Login? Login { get; set; }


    }
}
