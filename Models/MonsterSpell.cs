using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
    public class MonsterSpell
    {
        public int MonsterID { get; set; }
        public Monster? Monster { get; set; }

        public int SpellID { get; set; }
        public Spell? Spell { get; set; }
    }
}
