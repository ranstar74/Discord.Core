using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using Discord.Core.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Discord.Core.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _activityTimer;
        private readonly Users _user;

        public Servers _activeServer;

        public MainWindow(Users user)
        {
            InitializeComponent();
            DataContext = this;

            _activityTimer = new DispatcherTimer();
            _activityTimer.Interval = TimeSpan.FromSeconds(1);
            _activityTimer.Tick += ActivityTimerTick;
            _activityTimer.Start();

            _user = user;

            UpdateServers();
            OpenHomePage();
        }

        private void ActivityTimerTick(object sender, EventArgs e)
        {
            _user.LastActivity = DateTime.Now;
            DbUtils.SafeSave();
        }

        private void UpdateServers()
        {
            ServerControl.ItemsSource = _user.Servers.ToList();
        }

        private void AddNewServerClick(object sender, RoutedEventArgs e)
        {
            //new CreateNewServerWindow(_user).Show();

            // TODO: Make only on dialog result == true
            //UpdateServers();
        }

        private void ServerSelectClick(object sender, RoutedEventArgs e)
        {
            var oldServer = _activeServer;
            _activeServer = ((Button)sender).Tag as Servers;


            // Don't refresh server if user clicked on the same server
                if (oldServer != _activeServer)
            {
                ContentFrame.Content = new ServerPage(_user, _activeServer);
            }
        }

        private void HomePageClick(object sender, RoutedEventArgs e)
        {
            OpenHomePage();
        }

        private void OpenHomePage()
        {
            if (ContentFrame.Content?.GetType() != typeof(HomePage))
            {
                _activeServer = null;
                ContentFrame.Content = new HomePage();
            }
        }
    }
}
