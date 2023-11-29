

using FilmesApi.Views;
using Microsoft.EntityFrameworkCore;


namespace FilmesApi.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> ops) : base(ops)
    {
        
    }
    public DbSet <Movies> Movies { get; set; }
}
