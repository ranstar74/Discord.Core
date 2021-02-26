using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discord.Core.Entities.Tables
{
    public class Users
    {
        public Users()
        {
            Messages = new HashSet<Messages>();
            Servers = new HashSet<ServersToUsers>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Login { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public DateTime? LastActivity { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<ServersToUsers> Servers { get; set; }
    }
}
