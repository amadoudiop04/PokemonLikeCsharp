using Microsoft.Identity.Client;
using PokemonLikeCsharp.Models;
using PokemonLikeCsharp.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokemonLikeCsharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Settings.InitializeAndTestDatabase();
            InitializeComponent();
        }




        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var content = new PokemonContent())
                {

                    var user = content.Login.FirstOrDefault(u => u.Username == username);

                    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    {
                        MessageBox.Show("Connexion réussie.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                        Console.WriteLine("=== Informations utilisateur ===");
                        Console.WriteLine($"Nom d'utilisateur : {user.Username}");
                        Console.WriteLine($"Mot de passe haché : {user.PasswordHash}");
                        Console.WriteLine("================================");

                        // Naviguer vers la page d'accueil
                        // Exemple : NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Nom d'utilisateur ou mot de passe invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

 

    }
}
