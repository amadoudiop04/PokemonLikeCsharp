using CommunityToolkit.Mvvm.Input;
using PokemonLikeCsharp.Models;
using PokemonLikeCsharp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PokemonLikeCsharp.ViewModels
{
    public class SpellViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Spell> Spells { get; set; }
        public List<string> TrieOptions { get; set; }

        private string _selectedOption;
        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                if (_selectedOption != value)
                {
                    _selectedOption = value;
                    OnPropertyChanged(nameof(SelectedOption));
                    FilterSpells();
                }
            }
        }
        public ICommand BackCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SpellViewModel()
        {
            BackCommand = new RelayCommand(ExecuteBackCommand);

            TrieOptions = new List<string>
            {
                "Flamethrower", "Water Gun", "Vine Whip", "Sing",
                "Scratch", "Thunderbolt", "Confusion",
                "Karate Chop", "Body Slam", "Shadow Ball"
            };

            var spellService = new SpellService();
            Spells = new ObservableCollection<Spell>(spellService.GetAllSpells());
        }

        private void FilterSpells()
        {
            var spellService = new SpellService();
            Spells = new ObservableCollection<Spell>(
                spellService.GetAllSpells().Where(spell => spell.Name == SelectedOption).ToList()
            );
            OnPropertyChanged(nameof(Spells));
        }
        private void ExecuteBackCommand(object parameter)
        {
           
                var monsterView = new Views.MonsterView();
                monsterView.Show();

            Application.Current.Windows.OfType<SpellView>().FirstOrDefault()?.Close();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}