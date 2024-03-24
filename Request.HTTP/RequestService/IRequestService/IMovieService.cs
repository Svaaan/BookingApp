using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface IMovieService
    {
        Task<bool> PostMovie(MovieDTO movie);
        Task<List<MovieDTO>> GetMovie();
        Task<bool> RemoveMovieById(int movieId);
        Task<MovieDTO> EditMovieById(MovieDTO movie);
    }
}
