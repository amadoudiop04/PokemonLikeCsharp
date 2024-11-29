using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
    public class PlayerMonster
    {
        public int PlayerID { get; set; }
        public Player? Player { get; set; }

        public int MonsterID { get; set; }
        public Monster? Monster { get; set; }
    }
}
