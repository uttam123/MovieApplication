namespace MovieApplication.Api.Models
{
    public class Movie
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<string> Actors { get; set; }
        public string ReleaseDate { get; set; }
        public string PosterUrl { get; set; }
        public string Description { get; set; }
    }
}
