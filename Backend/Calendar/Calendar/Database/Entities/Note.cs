using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Database.Entities
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Date { get; set; }
        public NoteType Type { get; set; }
        public string Description { get; set; }

        public Note(string date, NoteType type = NoteType.Other, string description = "")
        {
            Date = date;
            Type = type;
            Description = description;
        }
    }

    public enum NoteType
    {
        Other = 0,
        Event = 1,
        Info = 2
    }
}
