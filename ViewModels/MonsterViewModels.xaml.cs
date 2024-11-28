using Microsoft.EntityFrameworkCore;
using PokemonLikeCsharp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokemonLikeCsharp.ViewModels
{
    /// <summary>
    /// Logique d'interaction pour MonsterViewModels.xaml
    /// </summary>
    public partial class MonsterViewModels : Window
    {

        public ObservableCollection<Monster> Monsters { get; set; }
        public ObservableCollection<MonsterSpell> MonsterSpells { get; set; }

        public MonsterViewModels()
        {
            InitializeComponent();
            DataContext = this;

                using (var content = new PokemonContent()) 
            {
                Monsters = new ObservableCollection<Monster>(content.Monster.ToList());
                //Monsters = new ObservableCollection<Monster>(content.Monster.Include(m => MonsterSpells).ToList());

            }
        }

    }
}
