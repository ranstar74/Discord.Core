using Discord.Core.Entities;
using System.Windows;

namespace Discord.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            DbUtils.DiscordDb?.Dispose();

            base.OnExit(e);
        }
    }
}
