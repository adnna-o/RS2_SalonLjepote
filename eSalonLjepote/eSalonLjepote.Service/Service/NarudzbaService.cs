using AutoMapper;
using eSaljonLjepote.Services.Service;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Service.Database;
using eSalonLjepote.Service.NarudzbaStateMachine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Service.Service
{
    public class NarudzbaService : BaseCRUDService<eSalonLjepote.Model.Models.Narudzba, eSalonLjepote.Service.Database.Narudzba, NarudzbaSearchRequest, NarudzbaInsertRequest, NarudzbaUpdateRequest>, INarudzbaService
    {
        public BaseNarudzbaState _baseState { get; set; }
        public NarudzbaService(BaseNarudzbaState baseState, ESalonLjepoteContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _baseState = baseState;
        }

        public void BeforeInsert(NarudzbaInsertRequest insert, Database.Narudzba entity)
        {
            entity.KorisnikId = insert.KorisnikId;
            entity.StatusNarudzbeId = insert.StatusNarudzbeId;
            entity.DatumNarudzbe = insert.DatumNarudzbe;
            entity.StateMachine = insert.StateMachine;
            entity.ProizvodId = insert.ProizvodId;

            base.BeforeInsert(insert, entity);

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
                if (!string.IsNullOrWhiteSpace(search?.NazivStatusa))
                {
                    filteredQuery = filteredQuery.Where(x => x.StatusNarudzbe.Naziv == search.NazivStatusa);
                }
                if (search?.StatusNarudzbeId != null && search.StatusNarudzbeId > 0)
                {
                    filteredQuery = filteredQuery.Where(x => x.StatusNarudzbeId == search.StatusNarudzbeId);
                }
            }

            return filteredQuery;
        }

        public override Model.Models.Narudzba Insert(NarudzbaInsertRequest insert)
        {
            int statusId;

            if (insert.StatusNarudzbeId.HasValue)
            {
                statusId = insert.StatusNarudzbeId.Value;

            }
            else
            {
                var naziv = string.IsNullOrWhiteSpace(insert.StatusNarudzbe)
                    ? "Kreirana"
                    : insert.StatusNarudzbe!;

                statusId = _context.Statuses
                                         .Where(s => s.Naziv == naziv)
                                         .Select(s => s.StatusNarudzbeId)
                                         .FirstOrDefault();

                if (statusId == 0)
                {
                    var s = new Status { Naziv = naziv };
                    _context.Statuses.Add(s);
                     _context.SaveChanges();
                    statusId = s.StatusNarudzbeId;
                }

                insert.StatusNarudzbeId = statusId;
            }

            var entity = _mapper.Map<Narudzba>(insert);
            entity.DatumNarudzbe ??= DateTime.Now;
            entity.StateMachine ??= "initial";
            entity.StatusNarudzbeId = statusId;
            entity.ProizvodId = insert.ProizvodId;
            entity.PlacanjeId = insert.PlacanjeId;
            entity.KolicinaProizvoda = insert.KolicinaProizvoda;
            entity.IznosNarudzbe = insert.IznosNarudzbe;

            _context.Narudzbas.Add(entity);
            _context.SaveChanges();


            return _mapper.Map<Model.Models.Narudzba>(entity);
        }


        public async Task<Model.Models.Narudzba> Checkout(NarudzbaCheckoutRequest req)
        {
            if (req == null || req.KorisnikId <= 0 || req.Stavke == null || req.Stavke.Count == 0)
                throw new ArgumentException("Prazna ili neispravna narudžba.");

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<Model.Models.Narudzba>(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    var nar = new Database.Narudzba
                    {
                        DatumNarudzbe = req.DatumNarudzbe ?? DateTime.Now,
                        KorisnikId = req.KorisnikId,
                        StatusNarudzbeId = req.StatusNarudzbeId ?? 1,
                        StateMachine = "Kreirana",
                        ProizvodId=req.Stavke.First().ProizvodId,
                        PlacanjeId=req.PlacanjeId ?? 1,
                    };

                    _context.Narudzbas.Add(nar);
                    await _context.SaveChangesAsync();

                    foreach (var s in req.Stavke)
                    {
                        var proizvod = await _context.Proizvods
                            .Where(j => j.ProizvodId == s.ProizvodId)
                            .Select(j => new { j.ProizvodId, j.Cijena })
                            .SingleOrDefaultAsync();

                        if (proizvod == null)
                            throw new Exception($"Jelo (ID={s.ProizvodId}) ne postoji.");

                        var cijenaInt = (int)Math.Round((double)(proizvod.Cijena));

                        _context.NarudzbaStavkas.Add(new Database.NarudzbaStavka
                        {
                            NarudzbaId = nar.NarudzbaId,
                            ProizvodId = s.ProizvodId,
                            KolicinaProizvoda = s.KolicinaProizvoda,
                            Cijena = cijenaInt,
                        });
                    }

                    await _context.SaveChangesAsync();
                    await tx.CommitAsync();

                    await _context.Entry(nar).Collection(n => n.Stavke).LoadAsync();

                    return _mapper.Map<Model.Models.Narudzba>(nar);
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<int> CheckoutFromCart(int korisnikId, int? statusId = null, string? paymentId = null, DateTime? datumNarudzbe = null, int? placanjeId=null)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<int>(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    Status status;
                    if (statusId.HasValue)
                    {
                        status = await _context.Statuses.FirstOrDefaultAsync(s => s.StatusNarudzbeId == statusId.Value)
                                 ?? throw new ArgumentException($"Status {statusId.Value} ne postoji.");
                    }
                    else
                    {
                        status = await _context.Statuses.FirstOrDefaultAsync(s => s.Naziv == "Kreirana");
                        if (status == null)
                        {
                            status = new Status { Naziv = "Kreirana" };
                            _context.Statuses.Add(status);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var stavkeKorpe = await _context.Korpas
                        .Where(k => k.KorisnikId == korisnikId)
                        .ToListAsync();

                    if (!stavkeKorpe.Any())
                        throw new InvalidOperationException("Korpa je prazna.");

                    var narudzba = new Narudzba
                    {
                        KorisnikId = korisnikId,
                        DatumNarudzbe = datumNarudzbe ?? DateTime.Now,
                        StatusNarudzbeId = status.StatusNarudzbeId,
                        StateMachine = "Kreirana",
                        ProizvodId=stavkeKorpe.First().ProizvodId,
                        KolicinaProizvoda = stavkeKorpe.Sum(k => k.KolicinaProizvoda ?? 1),
                        IznosNarudzbe = stavkeKorpe.Sum(k =>
                        {
                            int kol = k.KolicinaProizvoda ?? 1;

                            // Uzmi cijenu iz baze (uvijek tačna)
                            var cijena = _context.Proizvods
                                .Where(p => p.ProizvodId == k.ProizvodId)
                                .Select(p => p.Cijena)
                                .FirstOrDefault();

                            int cijenaInt = (int)Math.Round((double)cijena);

                            return cijenaInt * kol;
                        }),

                        Stavke = stavkeKorpe.Select(k =>
                        {
                            var proizvodCijena = _context.Proizvods
                                .Where(p => p.ProizvodId == k.ProizvodId)
                                .Select(p => p.Cijena)
                                .FirstOrDefault();

                            var cijenaInt = (int)Math.Round((double)proizvodCijena);

                            return new NarudzbaStavka
                            {
                                ProizvodId = k.ProizvodId,
                                KolicinaProizvoda = k.KolicinaProizvoda ?? 1,
                                Cijena = cijenaInt
                            };
                        }).ToList(),
                        PaymentId = paymentId,
                        PlacanjeId=placanjeId,

                    };

                    _context.Narudzbas.Add(narudzba);
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
        public async Task<List<Model.Models.Narudzba>> GetByKorisnik(int korisnikId)
        {
            var narudzbe = await _context.Narudzbas
                .Include(n => n.Stavke)
                .Include(n => n.Proizvod)
                .Where(n => n.KorisnikId == korisnikId)
                .OrderByDescending(n => n.DatumNarudzbe)
                .ToListAsync();

            return _mapper.Map<List<Model.Models.Narudzba>>(narudzbe);
        }





        /* public override Task<Model.Narudzba> Insert(NarudzbaInsertRequest insert)
         {
             var state = _baseState.CreateState("initial");

             return state.Insert(insert);

         }*/

        /* public override async Task<Model.Models.Narudzba> Update(int id, NarudzbaUpdateRequest update)
         {
             var entity = await _context.Narudzbas.FindAsync(id);

             var state = _baseState.CreateState(entity.StateMachine);

             return await state.Update(id, update);
         }*/

        public async Task<Model.Models.Narudzba> Activate(int id)
        {
            var entity = await _context.Narudzbas.FindAsync(id);

            var state = _baseState.CreateState(entity.StateMachine);

            return await state.Activate(id);
        }

        public async Task<Model.Models.Narudzba> Hide(int id)
        {
            var entity = await _context.Narudzbas.FindAsync(id);

            var state = _baseState.CreateState(entity.StateMachine);

            return await state.Hide(id);
        }

        public async Task<List<string>> AllowedActions(int id)
        {
            var entity = await _context.Narudzbas.FindAsync(id);
            var state = _baseState.CreateState(entity?.StateMachine ?? "initial");
            return await state.AllowedActions();
        }



        /* public async Task<Model.Models.Narudzba> Checkout(NarudzbaCheckoutRequest req)
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
         }*/




    }
}
