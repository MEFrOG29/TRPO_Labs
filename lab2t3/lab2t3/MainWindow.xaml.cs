using System.Windows;
using System.Windows.Media;

namespace lab2t3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            DefaultStyle();
            bool login = HasLogin(LoginTextBox.Text);
            bool pass = HasPassword(PasswordTextBox.Text);
            bool checkPass = HasCheckPassword(CheckPasswordTextBox.Text);
            bool email = HasEmail(EmailTextBox.Text);

            if( login && pass && checkPass && email)
            {
                MessageBox.Show("Успешная регистрация");
                return;
            }
            return;
        }

        bool HasLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                LoginTextBox.BorderBrush = Brushes.Red;
                LoginNullLabel.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }

        bool HasEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                EmailTextBox.BorderBrush = Brushes.Red;
                EmailNonValidLabel.Visibility = Visibility.Hidden;
                EmailNullLabel.Visibility = Visibility.Visible;
                return false;
            }

            if (!email.Contains('@'))
            {
                EmailTextBox.BorderBrush = Brushes.Red;
                EmailNullLabel.Visibility= Visibility.Hidden;
                EmailNonValidLabel.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }

        bool HasPassword(string pass)
        {
            var specialChars = new[] { '+', '-', '=', '*', '/', ',', '.', '!', '?', ':', ';' };
            bool hasDigit = pass.Any(char.IsDigit);
            bool hasLower = pass.Any(char.IsLower);
            bool hasUpper = pass.Any(char.IsUpper);
            bool hasSpecials = pass.Any(c => specialChars.Contains(c));
            bool hasCorrectLength = pass.Length >= 8 && pass.Length <= 30;

            if (string.IsNullOrWhiteSpace(pass))
            {
                PasswordTextBox.BorderBrush = Brushes.Red;
                PasswordNonValidLabel.Visibility = Visibility.Hidden;
                PasswordNullLabel.Visibility = Visibility.Visible;
                return false;
            }


            if (hasDigit && hasLower && hasUpper && hasSpecials && hasCorrectLength)
            {
                return true;
            }
            PasswordTextBox.BorderBrush = Brushes.Red;
            PasswordNullLabel.Visibility = Visibility.Hidden;
            PasswordNonValidLabel.Visibility = Visibility.Visible;
            return false;
        }

        bool HasCheckPassword(string checkPass)
        {
            if (string.IsNullOrWhiteSpace(checkPass))
            {
                CheckPasswordTextBox.BorderBrush = Brushes.Red;
                CheckPasswordNonValidLabel.Visibility = Visibility.Hidden;
                CheckPasswordNullLabel.Visibility = Visibility.Visible;
                return false;
            }

            if (PasswordTextBox.Text != checkPass)
            {
                CheckPasswordTextBox.BorderBrush = Brushes.Red;
                CheckPasswordNullLabel.Visibility= Visibility.Hidden;
                CheckPasswordNonValidLabel.Visibility = Visibility.Visible;
                return false;
            }

            return true;
        }

        private void DefaultStyle()
        {
            LoginTextBox.BorderBrush = Brushes.Black;
            PasswordTextBox.BorderBrush = Brushes.Black;
            CheckPasswordTextBox.BorderBrush = Brushes.Black;
            EmailTextBox.BorderBrush = Brushes.Black;

            LoginNullLabel.Visibility = Visibility.Hidden;
            PasswordNullLabel.Visibility = Visibility.Hidden;
            CheckPasswordNullLabel.Visibility = Visibility.Hidden;
            EmailNullLabel.Visibility = Visibility.Hidden;

            PasswordNonValidLabel.Visibility = Visibility.Hidden;
            CheckPasswordNonValidLabel.Visibility = Visibility.Hidden;
            EmailNonValidLabel.Visibility = Visibility.Hidden;
        }

    }
}