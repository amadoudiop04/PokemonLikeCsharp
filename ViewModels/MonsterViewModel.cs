using PokemonLikeCsharp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PokemonLikeCsharp.Views;

namespace PokemonLikeCsharp.ViewModels
{
    public class MonsterViewModel : BaseViewModel
    {
        public ObservableCollection<Monster> Monsters { get; set; } = new ObservableCollection<Monster>();

        public ICommand NavigateToSpellCommand { get; }
        public ICommand NavigateToCombatCommand { get; }

        public MonsterViewModel()
        {
           
            NavigateToSpellCommand = new RelayCommand(NavigateToSpell);
            NavigateToCombatCommand = new RelayCommand(NavigateToCombat);

            LoadMonsters();
            LoadMonsterImagesFromJson();
        }

        private void LoadMonsters()
        {
            try
            {
                using (var context = new PokemonContent())
                {
                    Monsters = new ObservableCollection<Monster>(
                        context.Monster.Include(m => m.Spell).ToList()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMonsterImagesFromJson()
        {
            string jsonFilePath = "C:\\Users\\AMADOU\\GitHub\\PokemonLikeCsharp\\Ressources\\Pokemon_url.json";

            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string json = File.ReadAllText(jsonFilePath);

                    var monstersFromJson = JsonConvert.DeserializeObject<List<Monster>>(json);

                    if (monstersFromJson != null)
                    {
                        foreach (var monster in Monsters)
                        {
                            var matchingMonster = monstersFromJson.FirstOrDefault(m => m.Name == monster.Name);
                            if (matchingMonster != null)
                            {
                                monster.ImageUrl = matchingMonster.ImageUrl;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image URLs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"JSON file not found at {jsonFilePath}.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NavigateToSpell(object parameter)
        {
            
                var spellView = new SpellView();
                spellView.Show();
     
            Application.Current.Windows.OfType<MonsterView>().FirstOrDefault()?.Close();
        }

        private void NavigateToCombat(object parameter)
        {
           
                var combatView = new CombatView();
                combatView.Show();

            Application.Current.Windows.OfType<MonsterView>().FirstOrDefault()?.Close();
        }
    }
}
