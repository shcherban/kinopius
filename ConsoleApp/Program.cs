using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Domain;
using Newtonsoft.Json;
using Domain.OMDb;
using Film = Domain.OMDb.Film;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter title: ");
            var title = Console.ReadLine();

            Engine engine = new Engine();
            SearchResult searchResult = engine.GetOMDbResponse(title);

            if (searchResult.Response)
            {
                foreach (Film film in searchResult.Search)
                {
                    DisplayFilm(film);
                    DisplayFilm(Domain.Film.FromOMDbFilm(film));
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine(searchResult.Error);
            }
        }

        private static void DisplayFilm(Film film)
        {
            //film = JsonConvert.DeserializeObject<Film>(json);

            if (film == null) return;
            Console.WriteLine("---------OMDbFilm----------");
            var props = film.GetType().GetProperties();
            foreach (var prop in props)
            {
                
                var value = prop.GetValue(film);
                if (value != null) Console.WriteLine($"{prop.Name} = {value}");
            }
            Console.WriteLine("-------------------");
        }

        private static void DisplayFilm(Domain.Film film)
        {
            if (film == null) return;
            Console.WriteLine("---------Film----------");
            var props = film.GetType().GetProperties();
            foreach (var prop in props)
            {
                
                var value = prop.GetValue(film);
                if (value != null) Console.WriteLine($"{prop.Name} = {value}");
            }
            Console.WriteLine("-------------------");
        }
    }
}