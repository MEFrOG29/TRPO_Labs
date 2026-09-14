using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab2t1
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        List<User> users = new List<User>
 {
     new User { Login = "vovik", Password = "123" },
     new User { Login = "petya", Password = "111" },
     new User { Login = "qwerty", Password = "000" }
 };

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in users)
            {
                if (user.Login == LoginTextBox.Text && user.Password == PasswordTextBox.Text)
                {
                    MainWindow window = new();
                    window.Show();
                    this.Close();
                }
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow window = new();
            window.Show();
            this.Close();
        }
    }
}
