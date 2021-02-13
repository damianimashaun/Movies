using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using MoviesApi.Interfaces;
using MoviesApi.Models;
using System.Collections.Generic;

namespace MoviesApi.Services
{
    public class ReaderService : IReaderService
    {
        private const string MetadataPath = "assets/metadata.csv";
        private const string StatsPath = "assets/stats.csv";

        public IEnumerable<MovieStats> MakeStats()
        {
            using (var reader = new StreamReader(MetadataPath))
            using (var metaReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            using (var statsStream = new StreamReader(StatsPath))
            using (var statsReader = new CsvReader(statsStream, CultureInfo.InvariantCulture))
            {
                var movies = metaReader.GetRecords<Movie>().ToList();
                var stats = statsReader.GetRecords<Stats>().ToList();

                var statsSummary = stats.GroupBy(x => x.movieId)
                .Select(x => new StatsSummary
                {
                    MovieId = x.First().movieId,
                    TotalWatchTime = x.Sum(x => x.watchDurationMs),
                    Watches = x.Count()
                });

                var result = new List<MovieStats>();
                foreach (var statsGroup in statsSummary)
                {
                    var movie = movies.FirstOrDefault(x => x.MovieId == statsGroup.MovieId);

                    if (movie != null)
                    {
                        result.Add(new MovieStats
                        {
                            MovieId = movie.MovieId,
                            Title = movie.Title,
                            AverageWatchDurationS = (statsGroup.TotalWatchTime / 1000) / statsGroup.Watches,
                            Watches = statsGroup.Watches,
                            ReleaseYear = movie.ReleaseYear
                        });
                    }
                }

                return result.OrderByDescending(x => x.Watches).ThenByDescending(x => x.ReleaseYear);
            }
        }
    }
}
