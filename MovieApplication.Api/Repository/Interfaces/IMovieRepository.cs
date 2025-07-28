using MovieApplication.Api.Models;

namespace MovieApplication.Api.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(Guid id);
        Task<List<Movie>> SearchAsync(string? name, string? genre, string? actor);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Guid id);
    }
}
