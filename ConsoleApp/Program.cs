using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

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

            HttpWebRequest request = WebRequest.CreateHttp($"http://www.omdbapi.com/?apikey={APIKEY}&t={searchString}");

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

            Film film = JsonConvert.DeserializeObject<Film>(json);

            Console.WriteLine(film.Title);
            Console.WriteLine(film.Year);
            Console.WriteLine(film.Director);
            Console.WriteLine(film.Writer);
            Console.WriteLine(film.Actors);
            Console.WriteLine(film.Plot);
            Console.WriteLine(film.Poster);

            Console.ReadKey();
        }
    }

    internal class Film
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public List<Rating> Ratings { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string DVD { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public string Response { get; set; }
    }

    internal class Rating
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}