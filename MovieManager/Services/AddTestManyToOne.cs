﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data.DataModels;
using MovieManager.Data.DBConfig;
using MovieManager.Models.DataModels;

namespace MovieManager.Services
{
    public class AddTestManyToOne
    {
        private readonly MovieContext data;

        public AddTestManyToOne(MovieContext data)
        {
            this.data = data; //access db without new contexts
        }


        public void AddMovieToPlaylist(int movieId, string PlaylistId)
        {
            
            var m = data.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
            if(m != null)
            {
                Console.WriteLine($"Movie {m.Title} found !!!");
                Console.WriteLine($"Movie ID {m.MovieId}");
                Console.WriteLine($"Movie poster - {m.PosterUrl}");
            }

            var playlist = data.Playlists
                .Include(a => a.Movies)         //deba prostotiqta
                .Where(p => p.PlaylistId == PlaylistId)
                .FirstOrDefault();
            
            if(playlist != null)
            {
                Console.WriteLine($"Playlist {playlist.PlaylistName} found");
            }

            playlist.Movies.Add(m);
            data.Playlists.Update(playlist);
            data.SaveChanges();

            var check = data.Playlists.Where(p => p.PlaylistId == PlaylistId).FirstOrDefault();
            Console.WriteLine($"Movie count in playlist: {check.Movies.Count}");
        }



        public void CreatePlaylist(string playlistName, string userId)
        {
            //IdentityUser user = data.Users.Where(u => u.Id == userId).FirstOrDefault();
            //get user id as this.User.Id so it only works when logged.

            Playlist playlist = new Playlist
            {
                PlaylistName = playlistName,
                //UserId = userId,
                Movies = new List<Movie>(),
                QrCode = "qr-code-bytes here",
            };

            var userPlaylist = data.UserPlaylists.Where(u => u.UserId == userId).FirstOrDefault();
            userPlaylist.Playlists.Add(playlist);
            Console.WriteLine($"Number of playlists for the user: {userPlaylist.Playlists.Count}");
            
            data.UserPlaylists.Update(userPlaylist);
            data.SaveChanges();
        }

        public void PrintAllPlaylistMovies()
        {
            MovieContext context = new MovieContext();


            foreach (var item in context.Playlists)
            {
                Console.WriteLine($"Playlist - {item.PlaylistName}");
                foreach (var movie in item.Movies)
                {
                    Console.WriteLine($"Movie Name - {movie.Title}");
                }
            }
        }
    }
}
