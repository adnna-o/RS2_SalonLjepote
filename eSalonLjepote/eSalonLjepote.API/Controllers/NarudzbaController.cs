using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eSalonLjepote.Model.Models;
using eSalonLjepote.Model;
using eSalonLjepote.Service.Database;

namespace eSalonLjepote.API.Controllers
{
    [Route("[controller]")]
    
    public class NarudzbaController : BaseCRUDController<Model.Models.Narudzba, NarudzbaSearchRequest, NarudzbaInsertRequest, NarudzbaUpdateRequest>
    {
        protected readonly INarudzbaService _service;
        public NarudzbaController(ILogger<BaseController<Model.Models.Narudzba, NarudzbaSearchRequest>> logger, INarudzbaService service) : base(logger, service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpGet("Izvjestaj")]
        public async Task<PagedResult<eSalonLjepote.Model.Models.Narudzba>> GetIzvjestaj([FromQuery] NarudzbaSearchRequest? search = null)
        {
            return await _service.Get(search);
        }

        /* [HttpPost("checkout")]
         public async Task<ActionResult<int>> Checkout([FromBody] NarudzbaCheckoutRequest request)
         {
             var narudzba = await _service.Checkout(request);
             return Ok(narudzba.NarudzbaId);

         }

         [HttpPost("checkoutFromCart")]
         public async Task<ActionResult<int>> CheckoutFromCart([FromBody] CheckoutFromCartRequest req)
         {
             if (req == null || req.KorisnikId <= 0)
                 return BadRequest("Neispravan zahtjev.");

             try
             {
                 var id = await _service.CheckoutFromCart(req.KorisnikId, req.PaymentId, req.DatumNarudzbe);
                 return Ok(id);
             }
             catch (InvalidOperationException ex)
             {
                 return BadRequest(ex.Message);
             }
             catch (Exception ex)
             {
                 return StatusCode(500, ex.Message);
             }
         }*/


        [HttpPost("checkout")]
        public async Task<ActionResult<int>> Checkout([FromBody] NarudzbaCheckoutRequest request)
        {
            var narudzba = await _service.Checkout(request);
            return Ok(narudzba.NarudzbaId);

        }

        [HttpPost("checkoutFromCart")]
        //[HttpPost("checkout-from-cart")]
        public async Task<ActionResult<int>> CheckoutFromCart([FromBody] CheckoutFromCartRequest req)
        {
            if (req == null || req.KorisnikId <= 0)
                return BadRequest("Neispravan zahtjev.");

            var id = await _service.CheckoutFromCart(req.KorisnikId, req.StatusId, req.PaymentId, req.DatumNarudzbe, req.PlacanjeId);
            return Ok(id);
        }


        [HttpPut("{id}/activate")]
        public virtual async Task<Model.Models.Narudzba> Activate(int id)
        {
            return await _service.Activate(id);
        }

        [HttpPut("{id}/hide")]
        public virtual async Task<Model.Models.Narudzba> Hide(int id)
        {
            return await _service.Hide(id);
        }

        [HttpGet("{id}/allowedActions")]
        public virtual async Task<List<string>> AllowedActions(int id)
        {
            return await _service.AllowedActions(id);
        }

        [HttpGet("korisnik/{korisnikId}")]
        public async Task<IActionResult> GetByKorisnik(int korisnikId)
        {
            var result = await _service.GetByKorisnik(korisnikId);
            return Ok(result);
        }

    }
}
