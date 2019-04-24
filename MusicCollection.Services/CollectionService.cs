using MusicCollection.DataAccess;
using MusicCollection.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MusicCollection.Services
{
    public class CollectionService
    {
        public static void ManagementConsole()
        {
            using (var context = new MusColectionContext())
            {
                List<string> singers=new List<string>(); 
                foreach (var songList in context.SongLists.Include(songList=> songList.Description).ToList())
                    singers.Add(songList.Singer);

                if (singers.Count == 0)
                {
                   
                    int choice = 0;
                    while (choice == 0)
                    {
                        Console.Clear();
                        ShowCollection();
                        Console.WriteLine("В Вашем альбоме нет ни одного трека, хотели бы добавить?\n" +
                       "1.да\n2.нет");
                        int.TryParse(Console.ReadLine(), out choice);

                        if (choice <= 0 || choice > 2) choice = 0;
                        else break;
                    }

                    if (choice == 2)
                    {
                        Console.WriteLine("До свидания!");
                    }

                    else
                    {
                        Add();
                    }
                }

                else
                {
                    bool display = true;
                    while (display == true)
                    {
                    Console.Clear();
                    ShowCollection();

                    Console.WriteLine("\n\n1.Добавить\n2.Удалить\n3.Обновить\n4.Выход");

                    int choice = 0;
                    while (choice==0)
                    {
                    int.TryParse(Console.ReadLine(), out choice);
                        if (choice <= 0 || choice > 4) choice = 0;
                        else if (choice == 4)
                        {
                                display = false;
                                break;
                        }
                        else break;
                    }

                        switch (choice)
                        {
                            case 1:
                                Add();
                                break;
                            case 2:
                                Console.WriteLine("Введите имя иполнителя песню котого вы хотели бы удалить:");
                                Delete(Console.ReadLine());
                                Console.Clear();
                                ShowCollection();
                                break;
                            case 3:
                                Console.WriteLine("Введите имя исполнителя, которое необходимо отредактировать:");
                                string oldSinger = Console.ReadLine();
                                Console.WriteLine("Введите новое имя редактируемого иполнителя:");
                                string newSinger = Console.ReadLine();
                                Console.WriteLine("Введите название песни:");
                                string newSong = Console.ReadLine();
                                Console.WriteLine("Введите длительность песни:");
                                string songLength = Console.ReadLine();
                                Console.WriteLine("Введите рейтинг песни:");
                                int rating = 0;
                                int.TryParse(Console.ReadLine(), out rating);
                                Console.WriteLine("Введите текст песни:");
                                string songText = Console.ReadLine();
                                Update(oldSinger, newSinger, newSong, songLength, rating, songText);
                                Console.Clear();
                                ShowCollection();
                                break;
                            case 4:
                                break;
                        }
                    }
                }
            }

           
        }

        #region отображение базы

       
        public static void ShowCollection()
        {
            Console.WriteLine("Музыкальная коллекция\t\tИсполнитель  Композиция  (описание: длительность,    рейтинг,    слова)");
            using (var context = new MusColectionContext())
            {
                foreach (var songList in context.SongLists.Include(songList => songList.Description).ToList())
                {
                    Console.WriteLine($"{songList.Singer}   {songList.Song} ({songList.Description.SongLehgth},   {songList.Description.Rating},   {songList.Description.SongText})");
                }
            }
        }
        #endregion

        #region добавление

        public static void Add()
        {
            using (var context = new MusColectionContext())
            {
                SongList songList = new SongList();
                SongDescription songDescription = new SongDescription();

                Console.Write("Ввдите исполнителя песни: ");
                songList.Singer = Console.ReadLine();
                Console.Write("Введите композицию: ");
                songList.Song = Console.ReadLine();
                Console.WriteLine("Описание");
                Console.Write("\tдлительность трека: ");
                songDescription.SongLehgth = Console.ReadLine();
                Console.Write("\tвведите рейтинг трека: ");
                int rating = 0;
                int.TryParse(Console.ReadLine(), out rating);
                if (rating == 0)
                {
                    Console.WriteLine("Вы не отметили рейтинг песни?");
                }
                Console.WriteLine("Слова песни по умолчанию. Cлова наиболее любимых треков можно будет занести позже");
                songDescription.SongText = "";

                context.SongLists.Add(songList);


                context.SongDescriptions.Add(songDescription);
                context.SaveChanges();
            }
        }
        #endregion

        #region удаление

        public static void Delete(string singer)
        {
            using (var context = new MusColectionContext())
            {
                SongList songList = context.SongLists.Include("Description").FirstOrDefault();
                if (songList != null)
                {
                    context.SongDescriptions.Remove(songList.Description);
                    context.SongLists.Remove(songList);
                    context.SaveChanges();
                }

                SongDescription songDescription = context.SongDescriptions.FirstOrDefault(description=> description.SongList.Singer==singer);
                if (songDescription!=null)
                {
                    context.SongDescriptions.Remove(songDescription);
                    context.SaveChanges();
                }
               
            }
        }
        #endregion

        #region редактирование

        public static void Update(string oldSinger, string newSinger, string newSong, string songLength, int rating, string songText)
        {
            using (var context = new MusColectionContext())
            {
                SongList songList = context.SongLists.FirstOrDefault(treck=> treck.Singer== oldSinger);
                if (songList != null)
                {
                    songList.Singer = newSinger;
                    songList.Song = newSong;
                    context.Entry(songList).State = EntityState.Modified;
                    context.SaveChanges();
                }

                SongDescription songDescription = context.SongDescriptions.FirstOrDefault(description => description.SongList.Singer == oldSinger);
                if (songDescription != null)
                {
                    songDescription.SongLehgth = songLength;
                    songDescription.Rating = rating;
                    songDescription.SongText = songText;
                    context.SaveChanges();
                }
                Console.Clear();
                ShowCollection();
            }
        }
        #endregion

    }
}
