using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eSalonLjepote.Service.Database;

namespace eSalonLjepote.API.Controllers
{
    [Route("[controller]")]
    public class KlijentiController : BaseCRUDController<Model.Models.Klijenti, KlijentiSearchRequest, KlijentiInsertRequest, KlijentiUpdateRequest>
    {

        public KlijentiController(ILogger<BaseController<Model.Models.Klijenti, KlijentiSearchRequest>> logger, IKlijentiService service) : base(logger, service)
        {

        }
       /* [HttpGet("GetByKorisnikId/{korisnikId}")]
        public ActionResult<Klijenti> GetByKorisnikId(int korisnikId)
        {
            var klijent = _service.GetByKorisnikId(korisnikId);
            if (klijent == null)
                return NotFound();
            return Ok(klijent);
        }*/




    }
}
