using System.Collections.Generic;

namespace Domain.OMDb
{
    public class SearchResult
    {
        public bool Response { get; set; }
        public string Error { get; set; }
        public List<Film> Search { get; set; }
    }
}