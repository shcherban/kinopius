using System;
using System.IO;
using System.Net;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Enter title: ");
            //var title = Console.ReadLine();

            string APIKEY = Environment.GetEnvironmentVariable("OMDbApiKey", EnvironmentVariableTarget.User);
            
            Console.WriteLine(APIKEY);

            HttpWebRequest request = WebRequest.CreateHttp($"http://www.omdbapi.com/?i=tt3896198&apikey={APIKEY}");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }

            Console.ReadKey();
        }
    }
}