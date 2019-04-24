using MusicCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection.DataAccess
{
    public class ActionsOnTheCollection
    {
        public static void Add()
        {
            using (var context=new MusColectionContext())
            {
                SongList songList = new SongList
                {
                    Singer= "The Script, will.i.am",
                    Song= "Hall of Fame"
                };

            }
        }
    }
}
