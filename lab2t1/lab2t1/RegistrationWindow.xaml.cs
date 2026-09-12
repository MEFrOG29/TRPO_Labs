using System.Windows;

namespace lab2t1
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new();
            window.Show();
            this.Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            bool hasName = !string.IsNullOrWhiteSpace(NameTextBox.Text);
            bool hasSurname = !string.IsNullOrWhiteSpace(SurnameTextBox.Text);
            bool hasLogin = !string.IsNullOrWhiteSpace(LoginTextBox.Text);
            bool hasPassword = !string.IsNullOrWhiteSpace(PasswordTextBox.Text);
            bool hasEmail = !string.IsNullOrWhiteSpace(EmailTextBox.Text);

            if (hasName && hasSurname && hasLogin && hasPassword && hasEmail)
            {
                MainWindow window = new();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля","Ошибка");
            }

        }


    }
}
