using PokemonLikeCsharp.ViewModels;
using System.Windows;

namespace PokemonLikeCsharp.Views
{
    public partial class SpellView : Window
    {
        public SpellView()
        {
            InitializeComponent();
            DataContext = new SpellViewModel();
        }
    }
}
