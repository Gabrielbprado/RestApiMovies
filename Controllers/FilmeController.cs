using FilmesApi.Data;
using FilmesApi.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : Controller
{
    private MovieContext _dbContext;

    public MovieController(MovieContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
        public IActionResult AddMovie([FromBody] Movies Movie)
    {
        _dbContext.Movies.Add(Movie);
        _dbContext.SaveChanges();
       
        return CreatedAtAction(nameof(GetMovieId),
                 new { id = Movie.Id },
                 Movie);

    }

    [HttpGet]
    public IEnumerable<Movies> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50) => _dbContext.Movies.Skip(skip).Take(take);

    [HttpGet("{id}")]
    public IActionResult GetMovieId(int id)
    {
        var filme = _dbContext.Movies.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

}
