using System.Collections.Generic;
using MoviesApi.Models;

namespace MoviesApi.Interfaces
{
    public interface IReaderService
    {
        IEnumerable<MovieStats> MakeStats();
    }
}
