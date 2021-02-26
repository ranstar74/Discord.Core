using Discord.Core.Entities.Tables;
using System;
using System.Linq;

namespace Discord.Core.Entities
{
    public delegate void RequestUserServersUpdate();

    /// <summary>
    /// <see cref="DiscordDb"/> related functions.
    /// </summary>
    public class DbUtils
    {
        /// <summary>
        /// Static connection for Discord DataBase.
        /// </summary>
        public static DiscordDb DiscordDb { get; private set; }

        public static Users Loggeduser { get; private set; }

        public static RequestUserServersUpdate RequrestUserServersUpdate { get; set; }

        /// <summary>
        /// Static constructor for initializing <see cref="DiscordDb"/> connection.
        /// </summary>
        static DbUtils()
        {
            DiscordDb = new DiscordDb();
        }

        /// <summary>
        /// Validates login and password.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="pass">User password.</param>
        /// <param name="user">Signed user.</param>
        /// <returns>True if successfully signed, otherwise False.</returns>
        public static bool Auth(string login, string pass, out Users user)
        {
            user = null;

            // If user exist in database
            if (!DiscordDb.Users.Any(x => x.Login == login))
                return false;

            // Get user with specified login
            var authUser = DiscordDb.Users.Single(x => x.Login == login);

            // If password matches
            if (authUser.Password != pass)
                return false;

            user = authUser;
            Loggeduser = authUser;

            return true;
        }

        /// <summary>
        /// Exception-safe save.
        /// </summary>
        /// <returns>True if saving was successful, otherwise False.</returns>
        public static bool SafeSave()
        {
            try
            {
                DiscordDb.SaveChanges();
            }
            catch (Exception)
            {
                throw;
                return false;
            }
            return true;
        }
    }
}
