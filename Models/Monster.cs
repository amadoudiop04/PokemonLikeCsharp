using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
   public class Monster
    {

        public int ID { get; set; }
        public string? Name { get; set; }
        public int Health { get; set; }
        public List<Spell>? Spell { get; set; }
    }
}
