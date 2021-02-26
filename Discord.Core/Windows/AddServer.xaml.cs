using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Discord.Core.Windows
{
    /// <summary>
    /// Interaction logic for AddServer.xaml
    /// </summary>
    public partial class AddServer : Window
    {
        /// <summary>
        /// Server name.
        /// </summary>
        public string ServerName { get; set; } = "";

        public AddServer()
        {
            InitializeComponent();

            DataContext = this;
        }

        /// <summary>
        /// Actions for creating new server.
        /// </summary>
        private void CreateServerClick(object sender, RoutedEventArgs e)
        {
            var newServer = new Servers()
            {
                Name = ServerName
            };
            newServer.Users.Add(new ServersToUsers()
            {
                Server = newServer,
                User = DbUtils.Loggeduser
            });


            DbUtils.DiscordDb.Servers.Add(newServer);
            if (DbUtils.SafeSave())
            {
                DbUtils.RequrestUserServersUpdate?.Invoke();
                Close();
            }
        }

        private void ServerNameTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CreateServerButton.IsEnabled = !string.IsNullOrEmpty(ServerName);

        }
    }
}
