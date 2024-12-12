using PokemonLikeCsharp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonLikeCsharp.ViewModels
{
    public class SpellService
    {
        public List<Spell> GetAllSpells()
        {
            using (var context = new PokemonContent())
            {
                return context.Spell.ToList();
            }
        }
    }
}
