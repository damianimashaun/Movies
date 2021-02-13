namespace MoviesApi.Models
{
    public class StatsSummary
    {
        public int MovieId { get; set; }
        public long TotalWatchTime { get; set; }
        public int Watches { get; set; }
    }
}
