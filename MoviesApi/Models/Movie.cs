using System.Text.Json.Serialization;

namespace MoviesApi.Models
{
    public class Movie
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }

        public bool IsValid()
        {
            if (Id < 1) return false;
            if (MovieId < 1) return false;
            if (string.IsNullOrWhiteSpace(Title)) return false;
            if (string.IsNullOrWhiteSpace(Language)) return false;
            if (string.IsNullOrWhiteSpace(Duration)) return false;
            if (ReleaseYear < 1) return false;

            return true;
        }
    }
}
