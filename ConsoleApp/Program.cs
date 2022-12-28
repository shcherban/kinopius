using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Domain.OMDb;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter title: ");
            var title = Console.ReadLine();
            var searchString = title.Replace(' ', '+');

            string APIKEY = Environment.GetEnvironmentVariable("OMDbApiKey", EnvironmentVariableTarget.User);

            Console.WriteLine(APIKEY);

            HttpWebRequest request = WebRequest.CreateHttp($"http://www.omdbapi.com/?apikey={APIKEY}&s={searchString}");

            string json;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }

            Console.WriteLine(json);

            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(json);

            foreach (Film film in searchResult.Search)
            {
                DisplayFilm(film);
                Console.ReadKey();
            }
        }

        private static void DisplayFilm(Film film)
        {
            //film = JsonConvert.DeserializeObject<Film>(json);

            if (film == null) return;
            Console.WriteLine("-------------------");
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