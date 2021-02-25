using Discord.Core.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Discord.Core.Entities.Tables
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }

        public string FormattedDate => Date.Formatted();

        public virtual Groups Group { get; set; }
        public virtual Users User { get; set; }
    }
}
