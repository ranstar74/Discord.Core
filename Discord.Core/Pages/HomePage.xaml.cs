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
        private readonly DispatcherTimer _userUpdateTimer; 
        private readonly ObservableCollection<Users> _users;

        public HomePage()
        {
            InitializeComponent();

            _userUpdateTimer = new DispatcherTimer();
            _userUpdateTimer.Tick += UpdateTimerTick;
            _userUpdateTimer.Interval = TimeSpan.FromSeconds(1);
            _userUpdateTimer.Start();

            _users = new ObservableCollection<Users>();

            UsersControl.ItemsSource = _users;
            UpdateUsers();
        }

        private void UpdateTimerTick(object sender, EventArgs e)
        {
            UpdateUsers();
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
    }
}
