using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Discord.Core.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly DispatcherTimer _UpdateTimer; 
        private readonly ObservableCollection<Users> _users;
        private readonly ObservableCollection<Servers> _servers;

        public HomePage()
        {
            InitializeComponent();

            _UpdateTimer = new DispatcherTimer();
            _UpdateTimer.Tick += UpdateTimerTick;
            _UpdateTimer.Interval = TimeSpan.FromSeconds(1);
            _UpdateTimer.Start();

            _users = new ObservableCollection<Users>();
            _servers = new ObservableCollection<Servers>();

            UsersControl.ItemsSource = _users;
            ServersControl.ItemsSource = _servers;

            Process();
        }

        private void UpdateTimerTick(object sender, EventArgs e) => Process();

        private void Process()
        {
            UpdateUsers();
            UpdateServers();
        }

        private void UpdateServers()
        {
            // TODO: Remake querry
            var servers = DbUtils.DiscordDb.Servers.Where(
                x => !x.Users.Select(x => x.User).Contains(DbUtils.Loggeduser)).ToList();

            servers.ForEach(x => 
            {
                if (!_servers.Contains(x))
                    _servers.Add(x);
            });
        }

        private void UpdateUsers()
        {
            // TODO: 
            var users = DbUtils.DiscordDb.Users.AsQueryable().Where(x => x.LastActivity != null).ToList();

            users = users.Where(x =>
            {
                // Force reload user
                DbUtils.DiscordDb.Entry(x).Reload();

                var timeFromLastActivity = DateTime.Now.Subtract((DateTime)x.LastActivity).Seconds;

                return timeFromLastActivity < 3;
            }).ToList();

            users.ForEach(u =>
            {
                if (!_users.Contains(u))
                {
                    _users.Add(u);
                }
            });

            var usersToRemove = new List<Users>();

            // Remove offline users.
            foreach (var user in _users)
            {
                if (!users.Contains(user))
                    usersToRemove.Add(user);
            }
            foreach(var userToRemove in usersToRemove)
            {
                _users.Remove(userToRemove);
            }

            OnlineCount.Text = _users.Count().ToString();
        }

        private void ServerMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var server = ((Border)sender).Tag as Servers;

            DbUtils.DiscordDb.ServersToUsers.Add(new ServersToUsers()
            {
                User = DbUtils.Loggeduser,
                Server = server
            });
            DbUtils.SafeSave();

            _servers.Remove(server);

            // Force reload user
            DbUtils.DiscordDb.Entry(DbUtils.Loggeduser).Reload();

            Process();

            DbUtils.RequrestUserServersUpdate?.Invoke();
        }
    }
}
