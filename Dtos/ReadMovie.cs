using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Dtos
{
    public class ReadMovie
    {
        
        public string? Title { get; set; }
        public string? Gener { get; set; }
        public int Duration { get; set; }

    }
}
