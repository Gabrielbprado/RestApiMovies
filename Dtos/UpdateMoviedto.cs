﻿using FilmesApi.Views;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Dtos
{
    public class UpdateMoviedto 
    {

        [Required(ErrorMessage = "The Title is Required")]
        public string? Title { get; set; }
        [StringLength(10, ErrorMessage = "The Gener must be between 4 and 10 caracteres")]
        [MinLength(4, ErrorMessage = "The Gener must be between 4 and 10 caracteres")]
        [Required]
        public string? Gener { get; set; }
        [Range(70, 500, ErrorMessage = "The Movie must be between 70 and 500 Minute")]
        [Required]
        public int Duration { get; set; }
    }
}
