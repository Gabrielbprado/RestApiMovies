using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Dtos;
using FilmesApi.Views;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : Controller
{
    private MovieContext _dbContext;
    private IMapper _mapper;
    public MovieController(MovieContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
        public IActionResult AddMovie([FromBody] CreatedMoviedtos Moviedto)
    {
        Movies Movie = _mapper.Map<Movies>(Moviedto);
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

    [HttpPut("{id}")]
    public IActionResult PUTMovie(int id, UpdateMoviedto moviedto)
    {
        var movie = _dbContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if(movie == null) return NotFound();
        _mapper.Map(moviedto, movie);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]

    public IActionResult PatchMovie(int id,JsonPatchDocument<UpdateMoviedto> patch)
    {
           var movie =  _dbContext.Movies.FirstOrDefault(movie => movie.Id == id);
           if(movie == null) return NotFound();
        var PatchMovie = _mapper.Map<UpdateMoviedto>(movie);
        patch.ApplyTo(PatchMovie, ModelState);
        if (!TryValidateModel(PatchMovie))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(PatchMovie, movie);
        _dbContext.SaveChanges();
        return NoContent();


    }

}
