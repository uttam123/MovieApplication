using System.Text.Json;
using MovieApplication.Api.Models;
using MovieApplication.Api.Repository.Interfaces;

namespace MovieApplication.Api.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private const string FilePath = "Data/movies.json";

        private async Task<List<Movie>> ReadFromFileAsync()
        {
            if (!File.Exists(FilePath)) return new List<Movie>();
            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Movie>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Movie>();
        }

        private async Task WriteToFileAsync(List<Movie> movies)
        {
            var json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task<List<Movie>> GetAllAsync() => await ReadFromFileAsync();

        public async Task<Movie?> GetByIdAsync(Guid id) =>
            (await ReadFromFileAsync()).FirstOrDefault(m => m.Id == id);

        public async Task<List<Movie>> SearchAsync(string? name, string? genre, string? actor)
        {
            var movies = await ReadFromFileAsync();
            return movies.Where(m =>
                (string.IsNullOrEmpty(name) || m.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(genre) || m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(actor) || m.Actors.Any(a => a.Contains(actor, StringComparison.OrdinalIgnoreCase)))
            ).ToList();
        }

        public async Task AddAsync(Movie movie)
        {
            var movies = await ReadFromFileAsync();
            movies.Add(movie);
            await WriteToFileAsync(movies);
        }

        public async Task UpdateAsync(Movie movie)
        {
            var movies = await ReadFromFileAsync();
            var index = movies.FindIndex(m => m.Id == movie.Id);
            if (index == -1) throw new Exception("Movie not found");
            movies[index] = movie;
            await WriteToFileAsync(movies);
        }

        public async Task DeleteAsync(Guid id)
        {
            var movies = await ReadFromFileAsync();
            var updated = movies.Where(m => m.Id != id).ToList();
            await WriteToFileAsync(updated);
        }
    }
}
