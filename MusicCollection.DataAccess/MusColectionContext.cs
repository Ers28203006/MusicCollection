namespace MusicCollection.DataAccess
{
    using MusicCollection.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MusColectionContext : DbContext
    {
        public MusColectionContext()
            : base("name=MusColectionContext")
        {
        }
        public DbSet<SongDescription> SongDescriptions { get; set; }
        public DbSet<SongList> SongLists { get; set; }
    }
}