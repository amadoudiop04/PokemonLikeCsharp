using PokemonLikeCsharp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PokemonLikeCsharp.ViewModels
{
    public partial class SpellViewModel : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Spell> Spells { get; set; }
        public List<string> TrieOptions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SpellViewModel()
        {
            InitializeComponent();
            DataContext = this;

           
            TrieOptions = new List<string>
            {
                "Flamethrower", "Water Gun", "Vine Whip", "Sing",
                "Scratch", "Thunderbolt", "Confusion",
                "Karate Chop", "Body Slam", "Shadow Ball"
            };

            using (var content = new PokemonContent())
            {
                Spells = new ObservableCollection<Spell>(content.Spell.ToList());
            }
        }

       
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        private void TrieOptionsLoad(string selectedOption)
        {
            if (TrieOptions.Contains(selectedOption))
            {
                using (var content = new PokemonContent())
                {
                    Spells = new ObservableCollection<Spell>(
                        content.Spell.Where(spell => spell.Name == selectedOption).ToList()
                    );
                }

                
                OnPropertyChanged(nameof(Spells));
            }
        }

        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem is string selectedOption)
            {
                TrieOptionsLoad(selectedOption);
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            var Back = new MonsterViewModels();
            Back.Show();
            this.Close();
        }
    }
}
