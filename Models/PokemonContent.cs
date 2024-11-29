﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLikeCsharp.Models
{
   public class PokemonContent : DbContext
    {
        public DbSet<Login> Login { get; set; } = null!;
        public DbSet<Player> Player { get; set; } = null!;
        public DbSet<Monster> Monster { get; set; } = null!;
        public DbSet<Spell> Spell { get; set; } = null!;

        public DbSet<MonsterSpell> MonsterSpell { get; set; } = null!;
        public DbSet<PlayerMonster> PlayerMonster { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonsterSpell>()
        .HasKey(ms => new { ms.MonsterID, ms.SpellID });

            modelBuilder.Entity<Monster>()
                .HasMany(m => m.Spell)
                .WithMany(s => s.Monster)
                .UsingEntity<MonsterSpell>(
                    j => j.HasOne(ms => ms.Spell).WithMany(),
                    j => j.HasOne(ms => ms.Monster).WithMany()
                );

            modelBuilder.Entity<PlayerMonster>()
       .HasKey(pm => new { pm.PlayerID, pm.MonsterID });

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Monsters)
                .WithMany(m => m.Players)
                .UsingEntity<PlayerMonster>(
                    j => j.HasOne(pm => pm.Monster).WithMany(),
                    j => j.HasOne(pm => pm.Player).WithMany()
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
