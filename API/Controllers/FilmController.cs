using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }
    
    [HttpGet(Name = "GetFilm")]
    public IEnumerable<Film> Get()
    {
        return Enumerable.Range(1, 2).Select(index => new Film
            {
                Id = index.ToString(),
                Title = "Title",
                ReleaseDate = DateTime.Today
            })
            .ToArray();
    }
}