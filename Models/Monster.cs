using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PokemonLikeCsharp.Models
{
    public class Monster : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        private int _health;
        public int Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;
                    OnPropertyChanged(nameof(Health));
                }
            }
        }

        // Points de vie initiaux
       
        public List<Spell>? Spell { get; set; }
        public ICollection<Player> Players { get; set; } = new List<Player>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
