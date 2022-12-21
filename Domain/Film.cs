using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Film
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public Rating Rating { get; set; }
    }

    public class Rating
    {
        public Param[] Params { get; set; }
        public Dictionary<Param, int> Rates { get; set; }

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
