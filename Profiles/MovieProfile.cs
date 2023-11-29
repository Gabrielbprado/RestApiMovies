namespace FilmesApi.Profiles;
using AutoMapper;
using FilmesApi.Dtos;
using FilmesApi.Views;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<CreatedMoviedtos, Movies>();
        CreateMap<UpdateMoviedto, Movies>();
        CreateMap<Movies, UpdateMoviedto>();
    }
}
