using Microsoft.EntityFrameworkCore;
using PokemonLikeCsharp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PokemonLikeCsharp.ViewModels
{
    public partial class CombatViewModels : Window, INotifyPropertyChanged
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
                if (_selectedPlayerMonster != value)
                {
                    _selectedPlayerMonster = value;
                    OnPropertyChanged(nameof(SelectedPlayerMonster));
                    UpdatePlayerMonsterDetails();
                }
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

        public CombatViewModels()
        {
            InitializeComponent();
            DataContext = this;

            PlayerMonsters = new ObservableCollection<Monster>();
            EnemyMonsters = new ObservableCollection<Monster>();

            LoadMonsters();
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

        private void UpdatePlayerMonsterDetails()
        {
            if (SelectedPlayerMonster != null)
            {
                MessageBox.Show($"Monstre sélectionné : {SelectedPlayerMonster.Name}, HP : {SelectedPlayerMonster.Health}");
            }
        }

        private void Combat(object sender, RoutedEventArgs e)
        {
            if (EnemyMonsters == null || EnemyMonsters.Count == 0)
            {
                MessageBox.Show("Aucun monstre disponible pour le combat !");
                return;
            }

            if (SelectedPlayerMonster == null)
            {
                MessageBox.Show("Veuillez sélectionner un monstre !");
                return;
            }

            Monster enemyMonster = EnemyMonsters.FirstOrDefault();

            if (enemyMonster == null) return;

            Spell playerSpell = SelectedPlayerMonster.Spell.FirstOrDefault();
            if (playerSpell != null)
            {
                MessageBox.Show($"Le joueur attaque avec {playerSpell.Name}, infligeant {playerSpell.Damage} dégâts !");
                enemyMonster.Health -= playerSpell.Damage;

                if (enemyMonster.Health <= 0)
                {
                    MessageBox.Show($"{enemyMonster.Name} a été vaincu !");
                    EnemyMonsters.Remove(enemyMonster);
                    Score++;
                    return;
                }
            }

            Spell enemySpell = enemyMonster.Spell.FirstOrDefault();
            if (enemySpell != null)
            {
                MessageBox.Show($"L'ennemi contre-attaque avec {enemySpell.Name}, infligeant {enemySpell.Damage} dégâts !");
                SelectedPlayerMonster.Health -= enemySpell.Damage;

                if (SelectedPlayerMonster.Health <= 0)
                {
                    MessageBox.Show($"{SelectedPlayerMonster.Name} a été vaincu !");
                    PlayerMonsters.Remove(SelectedPlayerMonster);

                    if (PlayerMonsters.Count == 0)
                    {
                        MessageBox.Show("Tous vos monstres ont été vaincus. La partie est terminée !");
                    }

                    return;
                }
            }
        }

        private void Rematch(object sender, RoutedEventArgs e)
        {
            if (PlayerMonsters.Count == 0 && EnemyMonsters.Count == 0)
            {
                LoadMonsters();
            }
            else
            {
                ResetMonsterHealth(PlayerMonsters);
                ResetMonsterHealth(EnemyMonsters);
            }

            Score = 0;
            MessageBox.Show("Le combat peut recommencer !");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var back = new SpellViewModel();
            back.Show();
            Close();
        }
    }
}
