using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord.Core.Entities.Tables
{
    public class Groups
    {
        public Groups()
        {
            Messages = new HashSet<Messages>();
            Users = new HashSet<GroupsToUsers>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<GroupsToUsers> Users { get; set; }
    }
}
