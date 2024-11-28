using PokemonLikeCsharp.Models;
using System;
using System.Linq;
using System.Windows;

namespace PokemonLikeCsharp.Services
{
    internal class Settings
    {
        public static void InitializeAndTestDatabase()
        {
            try
            {
                using (var content = new PokemonContent())
                {
                    // Test de la connexion à la base
                    if (content.Database.CanConnect())
                    {
                        MessageBox.Show("Connexion à la base de données réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Vérifie si des utilisateurs existent déjà
                        if (!content.Login.Any())
                        {
                            // Crée un utilisateur par défaut
                            var defaultUser = new Login
                            {
                                Username = "admin",
                                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") // Hachage du mot de passe
                            };

                            // Ajoute l'utilisateur à la base
                            content.Login.Add(defaultUser);
                            content.SaveChanges();

                            MessageBox.Show("Utilisateur par défaut créé : admin/admin123", "Initialisation", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Impossible de se connecter à la base de données.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'accès à la base : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
