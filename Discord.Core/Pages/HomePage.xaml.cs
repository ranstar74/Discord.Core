using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _userUpdateTimer.Interval = TimeSpan.FromSeconds(5);
            _userUpdateTimer.Start();

            _users = new ObservableCollection<Users>();
        }

        private void UpdateTimerTick(object sender, EventArgs e)
        {
            // TODO: Friends
            var users = DbUtils.DiscordDb.Users.Where(
                x => DateTime.Now - x.LastActivity < TimeSpan.FromSeconds(5)).ToList();

            users.ForEach(u =>
            {
                if (!_users.Contains(u))
                {
                    _users.Add(u);
                }
            });
            
            // Remove offline users.
            foreach(var user in _users)
            {
                if (!users.Contains(user))
                    _users.Remove(user);
            }

            OnlineCount.Text = _users.Count().ToString();
        }
    }
}
