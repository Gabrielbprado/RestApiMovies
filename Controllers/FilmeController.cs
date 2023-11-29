using FilmesApi.Views;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : Controller
{
    private static List<Movies> Movies = new List<Movies>();
    private static int Id = 1;
    [HttpPost]
        public IActionResult AddMovie([FromBody] Movies Movie)
    {
        Movie.Id = Id++;
        Movies.Add(Movie);
        return CreatedAtAction(nameof(GetMovieId),
                 new { id = Movie.Id },
                 Movie);

    }

    [HttpGet]
    public IEnumerable<Movies> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50) => Movies.Skip(skip).Take(take);

    [HttpGet("{id}")]
    public IActionResult GetMovieId(int id)
    {
        var filme = Movies.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

}
