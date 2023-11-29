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
    /// <summary>
    /// Constructor that initiates the attributes
    /// </summary>
    /// <param name="dbContext">Required Objects to create a Builder</param>
    /// /// <param name="mapper">Required Objects to create a Builder</param>
    /// <returns>IActionResult</returns>
    public MovieController(MovieContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Adds a movie to the database
    /// </summary>
    /// <param name="Moviedto">Object with the fields required to create a movie</param>
    /// <returns>IActionResult</returns>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
        public IActionResult PostMovie([FromBody] CreatedMoviedtos Moviedto)
    {
        Movies Movie = _mapper.Map<Movies>(Moviedto);
        _dbContext.Movies.Add(Movie);
        _dbContext.SaveChanges();
       
        return CreatedAtAction(nameof(GetMovieId),
                 new { id = Movie.Id },
                 Movie);

    }

    /// <summary>
    /// Grabs All Movies from the Database
    /// </summary>
    /// <param name="skip">Object with the optional fields to get a movie</param>
    /// <param name="take">Object with the optional fields to get a movie</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">If insertion is done successfully</response>

    [HttpGet]
    public IEnumerable<ReadMovie> GetFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
      return _mapper.Map<List<ReadMovie>>(_dbContext.Movies.Skip(skip).Take(take));
    }

    /// <summary>
    /// Picks up the movies with the id specified in the database
    /// </summary>
    /// <param name="id">Object with the required fields to get a movie</param>
   /// <returns>IActionResult</returns>
    /// <response code="200">If insertion is done successfully</response>

    [HttpGet("{id}")]
    public IActionResult GetMovieId(int id)
    {
        var movie = _dbContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        var filmeDto = _mapper.Map<ReadMovie>(movie);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Change a Movie with Specified Id
    /// </summary>
    /// <param name="id">Object with the required fields to update a Movie</param>
    /// /// <param name="moviedto">Object with the required fields to update a Movie</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If insertion is done successfully</response>

    [HttpPut("{id}")]
    public IActionResult PUTMovie(int id, UpdateMoviedto moviedto)
    {
        var movie = _dbContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if(movie == null) return NotFound();
        _mapper.Map(moviedto, movie);
        _dbContext.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Change only one element of the movie with a specified ID
    /// </summary>
    /// <param name="id">Object with the required fields to update a Movie</param>
    /// /// <param name="patch">Object with the required fields to update a Movie</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">If insertion is done successfully</response>

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
    /// <summary>
    /// Deletes a Movie
    /// </summary>
    /// <param name="id">Object with the required fields to delete a Movie</param>
 
    /// <returns>IActionResult</returns>
    /// <response code="204">If insertion is done successfully</response>

    [HttpDelete("{id}")]

        public IActionResult DeleteMovie(int id)
    {
        var movie = _dbContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
            _dbContext.Remove(movie);
        _dbContext.SaveChanges();
          return NoContent();
        
        {
            
        }
    }

}
