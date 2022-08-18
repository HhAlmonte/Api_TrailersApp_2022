using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Dto_s;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class TrailersController : BaseController
    {
        private readonly ITrailerRepository _trailerRepository;
        private readonly IUtilitiesRepository<TrailersEntities> _utilitiesRepository;

        public TrailersController(ITrailerRepository trailerRepository,
                                  IUtilitiesRepository<TrailersEntities> utilitiesRepository
            )
        {
            _trailerRepository = trailerRepository;
            _utilitiesRepository = utilitiesRepository;
        }

        [Authorize]
        [HttpPost("agregar")]
        public async Task<ActionResult<TrailerDto>> AddTrailer(TrailerDto trailer)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            /*var emailProvisional = "Hbalmontess272@gmail.com";*/

            var trailers = new TrailersEntities(trailer.TrailerName,
                                                trailer.Description,
                                                trailer.Author,
                                                email,
                                                trailer.Image,
                                                trailer.Link,
                                                DateTimeOffset.Now);

            await _trailerRepository.AddTrailers(trailers);

            return Ok(trailers);
        }

        [Authorize]
        [HttpPut("actualizar{id}")]
        public async Task<ActionResult<TrailerDto>> UpdateTrailer(string id, TrailerDto trailerDto)
        {
            var trailer = await _utilitiesRepository.GetItemByIdAsync(id);

            if(trailer == null)
            {
                return NotFound(new CodeErrorResponse(404));
            }

            trailer.TrailerName = trailerDto.TrailerName;
            trailer.Description = trailerDto.Description;
            trailer.Author = trailerDto.Author;
            trailer.Image = trailerDto.Image;
            trailer.Link = trailerDto.Link;

            var result = await _trailerRepository.UpdateTrailers(trailer);

            if (result == 0)
            {
                throw new Exception($"Ups!, hubo un error actualizando ${trailer.TrailerName}");
            }

            return Ok(trailer);
        }

        [Authorize]
        [HttpDelete("eliminar{id}")]
        public async Task<ActionResult<TrailersEntities>> DeleteTrailer(string id)
        {
            var trailer = await _utilitiesRepository.GetItemByIdAsync(id);

            var result = await _trailerRepository.DeleteTrailers(trailer);

            if(result == 0)
            {
                throw new Exception($"Ups!, hubo un error eliminando ${trailer.TrailerName}. Por favor intentelo de nuevo");
            }

            return Ok("El trailer se eliminó de manera satisfactoria");
        }

        [HttpGet("identificador{id}")]
        public async Task<ActionResult<TrailersEntities>> GetTrailerById(string id)
        {
            var result = await _utilitiesRepository.GetItemByIdAsync(id);

            if(result == null)
            {
                return NotFound(new CodeErrorException(404));
            }

            return Ok(result);
        }

        [HttpGet("titulo{title}")]
        public async Task<ActionResult<TrailersEntities>> GetTrailerByTitle(string title)
        {
            var trailers = await _utilitiesRepository.GetAllAsync();

            var trailer = from x in trailers
                          where x.TrailerName == title
                          select x;

            if(trailer == null)
            {
                return NotFound(new CodeErrorException(404));
            }

            return Ok(trailer);
        }

        [HttpGet("trailers")]
        public async Task<ActionResult<IReadOnlyList<TrailersEntities>>> GetAllTrailers()
        {
            var result = await _utilitiesRepository.GetAllAsync();

            if(result == null)
            {
                return NotFound(new CodeErrorException(404, "No hay trailers creados o no están a su alcanze"));
            }

            return Ok(result);
        }
    }
}
