using AutoMapper;
using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Service.Database;
using eSaljonLjepote.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eSalonLjepote.Service.Service
{
    public class NarudzbaService : BaseCRUDService<eSalonLjepote.Model.Models.Narudzba, eSalonLjepote.Service.Database.Narudzba, NarudzbaSearchRequest, NarudzbaInsertRequest, NarudzbaUpdateRequest>, INarudzbaService
    {
        public NarudzbaService(ESalonLjepoteContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override IQueryable<eSalonLjepote.Service.Database.Narudzba> AddInclude(
     IQueryable<eSalonLjepote.Service.Database.Narudzba> query,
     NarudzbaSearchRequest? search = null)
        {
            if (search?.isPlacanjeIncluded == true)
            {
                query = query.Include(n => n.Placanje);
            }
            if (search?.isProizvodIncluded == true)
            {
                query = query.Include(n => n.Proizvod);
            }
            if (search?.isKorisnikIncluded == true)
            {
                query = query.Include(n => n.Korisnik);
            }

            return query;
        }

        public override IQueryable<eSalonLjepote.Service.Database.Narudzba> AddFilter(
     IQueryable<eSalonLjepote.Service.Database.Narudzba> query,
     NarudzbaSearchRequest? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (search != null)
            {
                if (search.DatumNarudzbe.HasValue && search.DatumNarudzbe.Value != default(DateTime))
                {
                    var date = search.DatumNarudzbe.Value.Date;
                    var nextDay = date.AddDays(1);
                    filteredQuery = filteredQuery.Where(n =>
                        n.DatumNarudzbe.HasValue &&
                        n.DatumNarudzbe.Value >= date &&
                        n.DatumNarudzbe.Value < nextDay
                    );
                }

                if (search.DatumOd.HasValue)
                {
                    var dateFrom = search.DatumOd.Value.Date;
                    filteredQuery = filteredQuery.Where(n =>
                        n.DatumNarudzbe.HasValue &&
                        n.DatumNarudzbe.Value >= dateFrom
                    );
                }

                if (search.DatumDo.HasValue)
                {
                    var dateTo = search.DatumDo.Value.Date.AddDays(1);
                    filteredQuery = filteredQuery.Where(n =>
                        n.DatumNarudzbe.HasValue &&
                        n.DatumNarudzbe.Value < dateTo
                    );
                }

                if (search.KolicinaProizvoda.HasValue && search.KolicinaProizvoda.Value > 0)
                {
                    filteredQuery = filteredQuery.Where(n => n.KolicinaProizvoda == search.KolicinaProizvoda);
                }

                if (search.IznosNarudzbe.HasValue)
                {
                    filteredQuery = filteredQuery.Where(n => n.IznosNarudzbe == search.IznosNarudzbe);
                }

                if (search.IznosOd.HasValue)
                {
                    filteredQuery = filteredQuery.Where(n => n.IznosNarudzbe >= search.IznosOd.Value);
                }

                if (search.IznosDo.HasValue)
                {
                    filteredQuery = filteredQuery.Where(n => n.IznosNarudzbe <= search.IznosDo.Value);
                }

                if (!string.IsNullOrWhiteSpace(search.ImeKlijenta))
                {
                    filteredQuery = filteredQuery.Where(n =>
                        n.Korisnik != null && n.Korisnik.Ime.Contains(search.ImeKlijenta)
                    );
                }

                if (!string.IsNullOrWhiteSpace(search.PrezimeKlijenta))
                {
                    filteredQuery = filteredQuery.Where(n =>
                        n.Korisnik != null && n.Korisnik.Prezime.Contains(search.PrezimeKlijenta)
                    );
                }

                if (!string.IsNullOrWhiteSpace(search.SadrzajNarudzbe))
                {
                    filteredQuery = filteredQuery.Where(n =>
                        n.Proizvod != null && n.Proizvod.NazivProizvoda.Contains(search.SadrzajNarudzbe)
                    );
                }
            }

            return filteredQuery;
        }


        public async Task<Model.Models.Narudzba> Checkout(NarudzbaCheckoutRequest req)
        {
            if (req == null || req.KorisnikId <= 0 || req.Stavke == null || !req.Stavke.Any())
                throw new ArgumentException("Prazna ili neispravna narudžba.");

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<Model.Models.Narudzba>(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    var narudzba = new Database.Narudzba
                    {
                        KorisnikId = req.KorisnikId,
                        DatumNarudzbe = req.DatumNarudzbe ?? DateTime.Now,
                        PaymentId = req.PaymentId
                    };

                    _context.Narudzbas.Add(narudzba);
                    await _context.SaveChangesAsync(); 

                    foreach (var stavkaReq in req.Stavke)
                    {
                        var proizvod = await _context.Proizvods
                            .FirstOrDefaultAsync(p => p.ProizvodId == stavkaReq.ProizvodId);

                        if (proizvod == null)
                            throw new InvalidOperationException($"Proizvod sa ID {stavkaReq.ProizvodId} ne postoji.");

                        var stavka = new NarudzbaStavka
                        {
                            NarudzbaId = narudzba.NarudzbaId,
                            ProizvodId = stavkaReq.ProizvodId,
                            Kolicina = stavkaReq.Kolicina,
                            Cijena = stavkaReq.Cijena != 0 ? stavkaReq.Cijena : proizvod.Cijena,
                        };

                        _context.NarudzbaStavkas.Add(stavka);
                    }

                    await _context.SaveChangesAsync();

                    await tx.CommitAsync();

                    return _mapper.Map<Model.Models.Narudzba>(narudzba);
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }


        public async Task<int> CheckoutFromCart(int korisnikId, string? paymentId = null, DateTime? datumNarudzbe = null)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<int>(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    var stavkeKorpe = await _context.Korpas
                        .Where(k => k.KorisnikId == korisnikId)
                        .ToListAsync();

                    if (!stavkeKorpe.Any())
                        throw new InvalidOperationException("Korpa je prazna.");

                    var narudzba = new Narudzba
                    {
                        KorisnikId = korisnikId,
                        DatumNarudzbe = datumNarudzbe ?? DateTime.Now,
                        PaymentId = paymentId
                    };

                    _context.Narudzbas.Add(narudzba);
                    await _context.SaveChangesAsync(); 

                    foreach (var stavka in stavkeKorpe)
                    {
                        if (!_context.Proizvods.Any(p => p.ProizvodId == stavka.ProizvodId))
                            throw new InvalidOperationException($"Proizvod sa ID {stavka.ProizvodId} ne postoji.");

                        var narudzbaStavka = new NarudzbaStavka
                        {
                            NarudzbaId = narudzba.NarudzbaId,
                            ProizvodId = stavka.ProizvodId,
                            Kolicina = stavka.Kolicina,
                            Cijena = stavka.Cijena
                        };
                        _context.NarudzbaStavkas.Add(narudzbaStavka);
                    }

                    await _context.SaveChangesAsync();

                    _context.Korpas.RemoveRange(stavkeKorpe);
                    await _context.SaveChangesAsync();

                    await tx.CommitAsync();
                    return narudzba.NarudzbaId;
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }




    }
}
