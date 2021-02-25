using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discord.Core.Entities.Tables
{
    public class GroupsToUsers
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [Key]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Groups Group { get; set; }
    }
}
