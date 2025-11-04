using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eSalonLjepote.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace eSalonLjepote.API.Controllers
{
    [Route("[controller]")]

    public class PlacanjeController : BaseCRUDController<Model.Models.Placanje, PlacanjeSearchRequest, PlacanjeInsertRequest, PlacanjeUpdateRequest>
    {
        private readonly ESalonLjepoteContext _context;
        public PlacanjeController(ILogger<BaseController<Model.Models.Placanje, PlacanjeSearchRequest>> logger, IPlacanjeService service) : base(logger, service)
        {
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Placanje>>> GetPlacanja()
        {
            var lista = await _context.Placanjes.ToListAsync();
            return Ok(lista);
        }
    }
    }
