using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicCollection.Models
{
    public class SongDescription
    {
        [Key]
        [ForeignKey("SongList")]
        public int Id { get; set; }
        public string SongText { get; set; }
        public string SongLehgth { get; set; }
        public int Rating { get; set; }
        public SongList SongList { get; set; }
    }
}
