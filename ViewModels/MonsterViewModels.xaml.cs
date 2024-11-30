using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PokemonLikeCsharp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

    public partial class MonsterViewModels : Window
    {
        public ObservableCollection<string> ImageUrls { get; set; }
        public ObservableCollection<Monster> Monsters { get; set; }
        public Dictionary<string, string> MonsterImages { get; set; } //pas encore fonctionnelle

        public MonsterViewModels()
        {
            InitializeComponent();
            DataContext = this;


            using (var content = new PokemonContent())
            {
                Monsters = new ObservableCollection<Monster>(content.Monster.Include(m => m.Spell).ToList());

            }

            ImageUrls = new ObservableCollection<string>();
            LoadMonstersFromJson();

        }
        private void LoadMonstersFromJson()
        {
            string jsonFilePath = "C:\\Users\\AMADOU\\GitHub\\PokemonLikeCsharp\\Pokemon_urls.json"; // Assurez-vous que le fichier est accessible

            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);

                // Désérialiser le JSON en un objet
                var data = JsonConvert.DeserializeObject<MonsterData>(json);

                // Ajouter les URLs des images dans la collection ObservableCollection
                foreach (var url in data.Monsters)
                {
                    ImageUrls.Add(url);
                }
            }


        }

        private void NavigateToSpell(object sender, RoutedEventArgs e)
        {
            var spellViewModel = new SpellViewModel();
            spellViewModel.Show();
            this.Close();
        }

        private void NavigateToPage2(object sender, RoutedEventArgs e)
        {

        }
    }
}
