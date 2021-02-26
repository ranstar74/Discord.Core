using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Discord.Core.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent(); 
            DataContext = this;
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            var login = LoginEntry.Input;
            var pass = PasswordEntry.Input;

            if (!DbUtils.Auth(login, pass, out Users user))
            {
                LoginEntry.ErrorMessage = " - Неверные данные для входа или пароль.";
                PasswordEntry.ErrorMessage = " - Неверные данные для входа или пароль.";
                return;
            }

            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            Close();
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }
    }
}
