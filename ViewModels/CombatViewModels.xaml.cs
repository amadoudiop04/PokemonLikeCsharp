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

        private void Combat(object sender, RoutedEventArgs e)
        {
            if (EnemyMonsters == null || EnemyMonsters.Count == 0)
            {
                MessageBox.Show("Aucun monstre disponible pour le combat !");
                return;
            }

            if (PlayerMonsters == null || PlayerMonsters.Count == 0)
            {
                MessageBox.Show("Vous n'avez pas de monstre disponible !");
                return;
            }

            Monster playerMonster = PlayerMonsters.FirstOrDefault();
            Monster enemyMonster = EnemyMonsters.FirstOrDefault();

            if (playerMonster == null || enemyMonster == null) return;

            // Phase 1 : Attaque du joueur
            Spell playerSpell = playerMonster.Spell.FirstOrDefault();
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

            // Phase 2 : Contre-attaque de l'ennemi
            Spell enemySpell = enemyMonster.Spell.FirstOrDefault();
            if (enemySpell != null)
            {
                MessageBox.Show($"L'ennemi contre-attaque avec {enemySpell.Name}, infligeant {enemySpell.Damage} dégâts !");
                playerMonster.Health -= enemySpell.Damage;

                if (playerMonster.Health <= 0)
                {
                    MessageBox.Show($"{playerMonster.Name} a été vaincu !");
                    PlayerMonsters.Remove(playerMonster);

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

            MessageBox.Show(" Le combat peut recommencer !");
        }

        private void back(object sender, RoutedEventArgs e)
        {
            var back = new SpellViewModel();
            back.Show();    
            this.Close();   
        }
    }
}
