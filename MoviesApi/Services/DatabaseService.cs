using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using MoviesApi.Interfaces;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class DatabaseService : IDatabaseProvider
    {
        private readonly List<Movie> movies;

        public DatabaseService()
        {
            movies = new List<Movie>();
            LoadMovies();
        }

        public IEnumerable<Movie> GetMovies(int id)
        {
            var response = movies.Where(x => x.MovieId == id && x.IsValid())
                .OrderBy(x => x.Language)
                .ThenBy(x => x.Id)
                .GroupBy(x => x.Language)
                .Select(x => x.First());

            return response;
        }

        public void SaveMovie(Movie movie)
        {
            movie.Id = movies.Count;
            movies.Add(movie);
        }

        private void LoadMovies()
        {
            using (var reader = new StreamReader("assets/metadata.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                movies.AddRange(csv.GetRecords<Movie>());
            }
        }
    }
}
