using PokemonLikeCsharp.ViewModels;
using System.Windows;

namespace PokemonLikeCsharp.Views
{
    public partial class CombatView : Window
    {
        public CombatView()
        {
            InitializeComponent();
            DataContext = new CombatViewModel();
        }
    }

}

