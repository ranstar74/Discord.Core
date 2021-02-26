using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord.Core.Entities.Tables
{
    public class Servers
    {
        public Servers()
        {
            Messages = new HashSet<Messages>();
            Users = new HashSet<ServersToUsers>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<ServersToUsers> Users { get; set; }
    }
}
