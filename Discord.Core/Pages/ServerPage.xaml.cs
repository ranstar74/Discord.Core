using Discord.Core.Entities;
using Discord.Core.Entities.Tables;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Discord.Core.Pages
{
    /// <summary>
    /// Interaction logic for ServerPage.xaml
    /// </summary>
    public partial class ServerPage : Page
    {
        private readonly DispatcherTimer _msgRefreshTimer;
        private readonly ObservableCollection<Messages> _messages;

        private Users _user;
        public Servers Server { get; set; }

        public ServerPage(Users user, Servers server)
        {
            InitializeComponent();
            DataContext = this;

            _msgRefreshTimer = new DispatcherTimer();
            _msgRefreshTimer.Tick += UpdateTimerTick;
            _msgRefreshTimer.Interval = TimeSpan.FromSeconds(0.1f);
            _msgRefreshTimer.Start();
            _messages = new ObservableCollection<Messages>();

            _user = user;
            Server = server;

            MessagesControl.ItemsSource = _messages;
        }

        private void UpdateTimerTick(object sender, EventArgs e)
        {
            UpdateMessages();
        }

        private void UpdateMessages()
        {
            var messages = Server.Messages.ToList();

            messages.ForEach(m =>
            {
                if (!_messages.Contains(m))
                {
                    _messages.Add(m);
                }

                // Scroll to bottom only if scroll position is in max scroll position
                if (MessageScrollViewer.VerticalOffset == MessageScrollViewer.ScrollableHeight)
                    MessageScrollViewer.ScrollToBottom();
            });
        }

        /// <summary>
        /// Actions for sending message.
        /// </summary>
        private void MessageTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
                if (!string.IsNullOrEmpty(MessageTextBox.Text))
                {
                    var msg = new Messages()
                    {
                        User = _user,
                        Server = Server,
                        Text = MessageTextBox.Text,
                        Date = DateTime.Now
                    };
                    DbUtils.DiscordDb.Messages.Add(msg);
                    if (DbUtils.SafeSave())
                    {
                        MessageTextBox.Clear();
                    }
                }
        }
    }
}
