using System.Collections.Generic;

namespace Domain.OMDb
{
    public class SearchResult
    {
        //public bool Response;
        //public string Error;
        public List<Film> Search { get; set; }
    }
}