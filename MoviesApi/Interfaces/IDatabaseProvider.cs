using System.Collections.Generic;
using MoviesApi.Models;

namespace MoviesApi.Interfaces
{
    public interface IDatabaseProvider
    {
        void SaveMovie(Movie movie);
        IEnumerable<Movie> GetMovies(int id);
    }
}
