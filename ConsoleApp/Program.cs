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

            Console.WriteLine(film?.Title);
            Console.WriteLine(film?.Year);
            Console.WriteLine(film?.Director);
            Console.WriteLine(film?.Writer);
            Console.WriteLine(film?.Actors);
            Console.WriteLine(film?.Plot);
            Console.WriteLine(film?.Poster);

            try
            {
                Process.Start(film?.Poster);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}