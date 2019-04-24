using MusicCollection.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectionService.ManagementConsole();
            Console.ReadKey();
        }
    }
}
