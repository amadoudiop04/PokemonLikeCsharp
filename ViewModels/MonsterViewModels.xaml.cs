using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PokemonLikeCsharp.Models;

namespace PokemonLikeCsharp.ViewModels
{
    public partial class MonsterViewModels : Window
    {
        public ObservableCollection<string> ImageUrls { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Monster> Monsters { get; set; } = new ObservableCollection<Monster>();
        public Dictionary<string, string> MonsterImages { get; set; } = new Dictionary<string, string>(); 

        public MonsterViewModels()
        {
            InitializeComponent();
            DataContext = this;
           

            try
            {
               
                using (var context = new PokemonContent())
                {
                    Monsters = new ObservableCollection<Monster>(
                        context.Monster.Include(m => m.Spell).ToList()
                    );
                }

                
                LoadMonstersFromJson();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMonstersFromJson()
        {
            string jsonFilePath = "C:\\Users\\AMADOU\\GitHub\\PokemonLikeCsharp\\Ressources\\Pokemon_url.json";

            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string json = File.ReadAllText(jsonFilePath);

                  
                    var data = JsonConvert.DeserializeObject<MonsterData>(json);
                    if (data != null)
                    {
                      
                        foreach (var url in data.Monsters)
                        {
                            ImageUrls.Add(url);
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

        
        private void SetMonsterImage(string monsterName, string imageUrl)
        {
            if (!MonsterImages.ContainsKey(monsterName))
            {
                MonsterImages.Add(monsterName, imageUrl);
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
            var combatViewModel = new CombatViewModels();
            combatViewModel.Show();
            this.Close();
        }
    }
}
