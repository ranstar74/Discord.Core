using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using Discord.Core.Pages;
using System;
using System.Collections.ObjectModel;
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
        private readonly Users _user;

        public Groups _activeServer;

        public MainWindow(Users user)
        {
            InitializeComponent();
            DataContext = this;

            _user = user;

            UpdateServers();
        }

        private void UpdateServers()
        {
            GroupsControl.ItemsSource = _user.Groups.ToList();
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
            _activeServer = ((Button)sender).Tag as Groups;


            // Don't refresh messages if user clicked on the same server
                if (oldServer != _activeServer)
            {
                ContentFrame.Content = new ServerPage(_user, _activeServer);
                //_messages.Clear();
            }
        }
    }
}
