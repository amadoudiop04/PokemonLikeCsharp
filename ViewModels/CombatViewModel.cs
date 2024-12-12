
using Microsoft.EntityFrameworkCore;
using PokemonLikeCsharp.Models;
using PokemonLikeCsharp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace PokemonLikeCsharp.ViewModels
{
    public class CombatViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Monster> _playerMonsters;
        public ObservableCollection<Monster> PlayerMonsters
        {
            get => _playerMonsters;
            set
            {
                _playerMonsters = value;
                OnPropertyChanged(nameof(PlayerMonsters));
            }
        }

        private Monster _selectedPlayerMonster;
        public Monster SelectedPlayerMonster
        {
            get => _selectedPlayerMonster;
            set
            {
                _selectedPlayerMonster = value;
                OnPropertyChanged(nameof(SelectedPlayerMonster));
            }
        }

        private ObservableCollection<Monster> _enemyMonsters;
        public ObservableCollection<Monster> EnemyMonsters
        {
            get => _enemyMonsters;
            set
            {
                _enemyMonsters = value;
                OnPropertyChanged(nameof(EnemyMonsters));
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private Dictionary<int, int> _initialHealths = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand CombatCommand { get; }
        public ICommand RematchCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand CutSoundCommand { get; }

        private SoundPlayer _soundPlayer;

        public CombatViewModel()
        {
            CombatCommand = new RelayCommand(Combat, CanExecuteCombat);
            RematchCommand = new RelayCommand(Rematch);
            BackCommand = new RelayCommand(Back);
            CutSoundCommand = new RelayCommand(CutSound);

            LoadMonsters();
            PlayMusic();
        }

        private void LoadMonsters()
        {
            try
            {
                using (var context = new PokemonContent())
                {
                    var allMonsters = new ObservableCollection<Monster>(
                        context.Monster.Include(m => m.Spell).ToList());

                    foreach (var monster in allMonsters)
                    {
                        _initialHealths[monster.ID] = monster.Health;
                    }

                    PlayerMonsters = new ObservableCollection<Monster>(allMonsters.Take(allMonsters.Count / 2));
                    EnemyMonsters = new ObservableCollection<Monster>(allMonsters.Skip(allMonsters.Count / 2));
                }

                Score = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}");
            }
        }

        private void Combat(object parameter)
        {
            if (EnemyMonsters == null || EnemyMonsters.Count == 0 || SelectedPlayerMonster == null) return;

            Monster enemyMonster = EnemyMonsters.FirstOrDefault();
            if (enemyMonster == null) return;

            Spell playerSpell = SelectedPlayerMonster.Spell.FirstOrDefault();
            if (playerSpell != null)
            {
                enemyMonster.Health -= playerSpell.Damage;

                if (enemyMonster.Health <= 0)
                {
                    EnemyMonsters.Remove(enemyMonster);
                    Score++;
                    return;
                }
                ShootMusic();
            }

            Spell enemySpell = enemyMonster.Spell.FirstOrDefault();
            if (enemySpell != null)
            {
                SelectedPlayerMonster.Health -= enemySpell.Damage;

                if (SelectedPlayerMonster.Health <= 0)
                {
                    PlayerMonsters.Remove(SelectedPlayerMonster);
                }
            }
        }

        private bool CanExecuteCombat(object parameter)
        {
            return SelectedPlayerMonster != null && EnemyMonsters?.Count > 0;
        }

        private void Rematch(object parameter)
        {
            ResetMonsterHealth(PlayerMonsters);
            ResetMonsterHealth(EnemyMonsters);
            Score = 0;
        }

        private void Back(object parameter)
        {
            var backView = new SpellView();
            backView.Show();
           
            Application.Current.Windows.OfType<CombatView>().FirstOrDefault()?.Close();
        }



        private void PlayMusic()
        {
            try
            {
                string soundFilePath = "C:\\Users\\AMADOU\\GitHub\\PokemonLikeCsharp\\Ressources\\music\\PokemonSound.wav";

                if (File.Exists(soundFilePath))
                {
                    _soundPlayer = new SoundPlayer(soundFilePath);
                    _soundPlayer.PlayLooping(); 
                }
                else
                {
                    MessageBox.Show($"Le fichier audio '{soundFilePath}' est introuvable.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de la lecture du fichier audio : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CutSound(object parameter)
        {
            try
            {
                _soundPlayer?.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de l'arrêt de la musique : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShootMusic()
        {
            try
            {
                string soundFilePath = "C:\\Users\\AMADOU\\GitHub\\PokemonLikeCsharp\\Ressources\\music\\Absorb.wav";

                if (File.Exists(soundFilePath))
                {
                    var shootPlayer = new SoundPlayer(soundFilePath);
                    shootPlayer.Play();
                }
                else
                {
                    MessageBox.Show($"Le fichier audio '{soundFilePath}' est introuvable.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de la lecture du fichier audio : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ResetMonsterHealth(ObservableCollection<Monster> monsters)
        {
            foreach (var monster in monsters)
            {
                if (_initialHealths.TryGetValue(monster.ID, out int initialHealth))
                {
                    monster.Health = initialHealth;
                }
            }
        }
    }
}
