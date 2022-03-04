﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Data.DataModels
{
    public class Movie
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }
        //this is the ID generated from TMDB

        [Required]
		public string Title { get; set; }

		[Required]
        public string PosterUrl { get; set; }

        [Required]
        public string Overview { get; set; }
        public decimal Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? MediaType { get; set; } //TV/Movie/Episode
        public decimal? Popularity { get; set; }


        public int? LanguageId { get; set; } //change to string?
        public int? PlatformId { get; set; }

        public string? Actors { get; set; }
        //many to many
        public string? Genre { get; set; }


        public Playlist? Playlist { get; set; }
        //public IEnumerable<Playlist> Playlists { get; init; } = new List<Playlist>();
    }
}
