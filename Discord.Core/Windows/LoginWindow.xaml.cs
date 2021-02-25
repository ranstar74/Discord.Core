using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

            var msg = DbUtils.DiscordDb.Messages.ToList();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
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
    }
}
