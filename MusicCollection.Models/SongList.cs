namespace MusicCollection.Models
{
    public class SongList
    {
        public int Id { get; set; }
        public string Singer { get; set; }
        public  string Song { get; set; }
        public SongDescription Description { get; set; }
    }
}
