using System;
using System.IO;
using System.Net;
using Domain.OMDb;
using Newtonsoft.Json;

namespace Domain
{
    public class Engine
    {
        public SearchResult GetOMDbResponse(string searchString)
        {
            searchString = searchString.Replace(' ', '+');

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

            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(json);
            return searchResult;
        }
    }
}