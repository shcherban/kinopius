namespace Domain;

public class Film
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Description { get; set; }
    public Rate Rate { get; set; }
}

public class Rate
{
    public Param[] Params { get; set; }
    public Dictionary<Param, int> Rates { get; set; }

    public Rate(Param[] @params, int[] rates)
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