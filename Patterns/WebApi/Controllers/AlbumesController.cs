using DataAccess.Generic;
using Entities.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumesController : ControllerBase
    {
        private readonly IGenericRepository<Album> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlbumesController(IGenericRepository<Album> genericRepository, IUnitOfWork unitOfWork) 
        {
            this._genericRepository = genericRepository;
            this._unitOfWork = unitOfWork;
        } 

        [HttpGet]
        public async Task<IEnumerable<Album>> Get()
        {
            return await _genericRepository.GetAsync(a => a.Anio > 1990, a => a.OrderByDescending(b => b.Anio), "Artista");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _genericRepository.CreateAsync(album);

            if(created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }
    }
}
