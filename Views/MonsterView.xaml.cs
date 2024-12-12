using System.Windows;
using PokemonLikeCsharp.ViewModels;

namespace PokemonLikeCsharp.Views
{
    public partial class MonsterView : Window
    {
        public MonsterView()
        {
            InitializeComponent();
            DataContext = new MonsterViewModel();
        }
    }
}
