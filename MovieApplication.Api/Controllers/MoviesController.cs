using Microsoft.AspNetCore.Mvc;
using MovieApplication.Api.Models;
using MovieApplication.Api.Repository.Interfaces;

namespace MovieApplication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repo;

        public MoviesController(IMovieRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var movie = await _repo.GetByIdAsync(id);
            return movie == null ? NotFound() : Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            await _repo.AddAsync(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Movie updatedMovie)
        {
            if (id != updatedMovie.Id) return BadRequest();
            await _repo.UpdateAsync(updatedMovie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? genre, [FromQuery] string? actor)
        {
            var result = await _repo.SearchAsync(name, genre, actor);
            return Ok(result);
        }
    }
}
