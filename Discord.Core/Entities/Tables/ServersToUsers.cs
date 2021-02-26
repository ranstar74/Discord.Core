using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discord.Core.Entities.Tables
{
    public class ServersToUsers
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [Key]
        public int ServerId { get; set; }
        [ForeignKey("ServerId")]
        public virtual Servers Server { get; set; }
    }
}
