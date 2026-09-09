using Microsoft.Win32;
using System.IO;
using System.Security.RightsManagement;
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


namespace lab2t1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string FilePath = "D:\\Saves\\other\\TRPO\\lab2t1\\users.csv";

        public MainWindow()
        {
            InitializeComponent();

            LoadUsersFromFile(FilePath);
        }

        public class User
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Login { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*"
            };

            if(ofd.ShowDialog() == true)
            {
                LoadUsersFromFile(ofd.FileName);
            }
        }

        private void LoadUsersFromFile(string filePath)
        {
            List<User> loadedUsers = new List<User>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split(';');
                    if (parts.Length == 5)
                    {
                        User user = new User
                        {
                            Name = parts[0],
                            Surname = parts[1],
                            Login = parts[2],
                            Email = parts[3],
                            Password = parts[4]
                        };
                        loadedUsers.Add(user);
                    }
                }
                UsersList.ItemsSource = loadedUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}");
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";


            if(sfd.ShowDialog() == true)
            {
                File.WriteAllLines(sfd.FileName, );
            }

        }
    }
}