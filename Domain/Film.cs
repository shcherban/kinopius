using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Film
    {
        public string? Id { get; set; }
        public Dictionary<string, string> Titles { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public Rating Rating { get; set; }

        public string FilmInfo => $"{Titles["en"]}{Environment.NewLine}{Year.ToString()}{Environment.NewLine}{Description}";

        public Film()
        {
            Titles = new Dictionary<string, string>();
            Rating = new Rating();
        }
        
        public  OMDb.Film OMDbFilm { get; set; }
        public OMDb.Film ToOMDbFilm()
        {
            var result = new OMDb.Film();
            result.imdbID = this.Id;
            return result;
        }

        public static Film FromOMDbFilm(OMDb.Film omdbFilm)
        {
            var result = new Film();
            result.Titles.Add("en", omdbFilm.Title);
            result.Description = omdbFilm.Plot;
            result.Id = omdbFilm.imdbID;
            DateTime releaseDate;
            bool releaseDateParseResult = DateTime.TryParse(omdbFilm.Released, out releaseDate);
            if (releaseDateParseResult) result.ReleaseDate = releaseDate;
            int year;
            bool yearParseResult = Int32.TryParse(omdbFilm.Year, out year);
            if (yearParseResult) result.Year = year;
            return result;
        }
    }

    public class Rating
    {
        public Param[] Params { get; set; }
        public Dictionary<Param, int> Rates { get; set; }

        public Rating()
        {
            Rates = new Dictionary<Param, int>();
            Params = new Param[2];
        }
        
        public Rating(Param[] @params, int[] rates)
        {
            Rates = new Dictionary<Param, int>();
            Enumerable.Zip<Param, int, int>(@params, rates, (p, r) =>
            {
                Rates.Add(p, r);
                return 0;
            });
        }
    }

    public enum Param
    {
        Direction,
        Acting,
        Plot
    }
}
