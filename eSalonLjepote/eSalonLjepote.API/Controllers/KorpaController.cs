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
    public class KorpaController : BaseCRUDController<Model.Models.Korpa, KorpaSearchObject, KorpaInsertRequest, KorpaUpdateRequest>
    {
        protected IKorpaService korpaService;
        ESalonLjepoteContext _context;
        public KorpaController(ILogger<BaseController<Model.Models.Korpa, KorpaSearchObject>> logger, IKorpaService service) : base(logger, service)
        {
            korpaService=service;
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            try
            {
                var korisnik = await _context.Korisniks.FindAsync(request.KorisnikId);
                if (korisnik == null)
                    return BadRequest("Nepostojeći korisnik.");

                var korpaStavke = await _context.Korpas
                    .Where(k => k.KorisnikId == request.KorisnikId)
                    .Include(k => k.Proizvod)
                    .ToListAsync();

                if (korpaStavke == null || !korpaStavke.Any())
                    return BadRequest("Korpa je prazna.");

                // Kreiraj narudžbu
                var narudzba = new Narudzba
                {
                    KorisnikId = request.KorisnikId,
                    DatumNarudzbe = request.DatumNarudzbe ?? DateTime.Now,
                    PlacanjeId = request.PlacanjeId ?? 1, // default gotovina
                    IznosNarudzbe = korpaStavke.Sum(s => (s.Proizvod?.Cijena ?? 0) * (s.KolicinaProizvoda ?? 0))
                };

                _context.Narudzbas.Add(narudzba);
                await _context.SaveChangesAsync();

                // Dodaj stavke iz korpe u NarudzbaStavka
                foreach (var item in korpaStavke)
                {
                    var stavka = new NarudzbaStavka
                    {
                        NarudzbaId = narudzba.NarudzbaId,
                        ProizvodId = item.ProizvodId,
                        KolicinaProizvoda = item.KolicinaProizvoda ?? 1,
                        Cijena = item.Proizvod?.Cijena ?? 0
                    };
                    _context.NarudzbaStavkas.Add(stavka);
                }

                // Očisti korpu
                _context.Korpas.RemoveRange(korpaStavke);
                await _context.SaveChangesAsync();

                return Ok(narudzba.NarudzbaId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Server side error", details = ex.Message });
            }
        }


    }
}
