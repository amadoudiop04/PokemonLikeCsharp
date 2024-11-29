using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
   public class Spell
    {
        public int ID { get; set; }
        public string? Name { get; set; }   
        public int Damage { get; set; }
        public string? Description { get; set; }
        public List<Monster>? Monster { get; set;}
    }
}
