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
using eSalonLjepote.Service.RabbitMQ;
using eSalonLjepote.Model.Models;

namespace eSalonLjepote.Service.Service
{
    public class TerminiService : BaseCRUDService<Model.Models.Termini, eSalonLjepote.Service.Database.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiUpdateRequest>, ITerminiService
    {
        private readonly IMailProducer _mailProducer;

        public TerminiService(IMailProducer mailProducer, ESalonLjepoteContext context, IMapper mapper)
            : base(context, mapper)
        {
            _mailProducer = mailProducer;
        }

       /* public override void BeforeInsert(TerminiInsertRequest insert, Database.Termini entity)
        {
            entity.DatumTermina = insert.DatumTermina;
            entity.VrijemeTermina = insert.VrijemeTermina;
            entity.UslugaId = insert.UslugaId;
            entity.ZaposleniId = insert.ZaposleniId;
            entity.KlijentId = insert.KlijentId;
            entity.BrojTransakcije = insert.BrojTransakcije;

            base.BeforeInsert(insert, entity);

            SendEmailOnTerminInsert(entity.KlijentId, entity.DatumTermina, entity.VrijemeTermina);
        }*/

        private void SendEmailOnTerminInsert(int klijentId, DateTime? datum, TimeSpan? vrijeme)
        {
            var klijent = _context.Klijentis
        .Include(k => k.Korisnik)
        .FirstOrDefault(k => k.KlijentId == klijentId);

            if (klijent?.Korisnik != null)
            {
                var user = klijent.Korisnik;

                var emailMessage = new
                {
                    Sender = "testadna.98@gmail.com",
                    Recipient = user.Email,
                    Subject = "Novi Termin Zakazan",
                    Content = $"Poštovani {user.Ime}, vaš termin je zakazan za {datum:dd.MM.yyyy} u {vrijeme}"
                };

                _mailProducer.SendEmail(emailMessage);
            }
        }
        public override IQueryable<eSalonLjepote.Service.Database.Termini> AddInclude(IQueryable<eSalonLjepote.Service.Database.Termini> query, TerminiSearchRequest? search = null)
        {
            if (search?.isZaposleniIncluded == true)
            {
                query = query.Include("Zaposleni");
            }
            if (search?.isKlijentiIncluded == true)
            {
                query = query.Include("Klijent");
            }
            if (search?.isUslugaIncluded == true)
            {
                query = query.Include("Usluga");
            }
            return base.AddInclude(query, search);
        }

        public override IQueryable<eSalonLjepote.Service.Database.Termini> AddFilter(
    IQueryable<eSalonLjepote.Service.Database.Termini> query,
    TerminiSearchRequest? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.NazivUsluge))
            {
                filteredQuery = filteredQuery.Where(x => x.Usluga.NazivUsluge.Contains(search.NazivUsluge.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(search?.ImeKlijenta))
            {
                filteredQuery = filteredQuery.Where(x => x.Klijent.Korisnik.Ime.Contains(search.ImeKlijenta.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.PrezimeKlijenta))
            {
                filteredQuery = filteredQuery.Where(x => x.Klijent.Korisnik.Prezime.Contains(search.PrezimeKlijenta.ToLower()));
            }

            if (search != null)
            {
                if (search.DatumTermina.HasValue && search.DatumTermina.Value != default(DateTime))
                {
                    var date = search.DatumTermina.Value.Date;
                    var nextDay = date.AddDays(1);
                    filteredQuery = filteredQuery.Where(n =>
                        n.DatumTermina.HasValue &&
                        n.DatumTermina.Value >= date &&
                        n.DatumTermina.Value < nextDay
                    );
                }

            }
                return filteredQuery;
        }

       public async Task<Model.Models.Termini> Insert(TerminiInsertRequest insert)
        {
            var entity = _mapper.Map<Database.Termini>(insert);

            // Postavljamo samo FK vrijednosti i ostale podatke
            entity.DatumTermina = insert.DatumTermina;
            entity.VrijemeTermina = insert.VrijemeTermina;
            entity.UslugaId = insert.UslugaId;
            entity.ZaposleniId = insert.ZaposleniId;
            entity.KlijentId = insert.KlijentId;
            entity.BrojTransakcije = insert.BrojTransakcije;

            _context.Set<Database.Termini>().Add(entity);
            await _context.SaveChangesAsync();

            // Tek sada možemo sigurno dohvatiti navigacijski property
            var klijent = _context.Klijentis
                .Include(k => k.Korisnik)
                .FirstOrDefault(k => k.KlijentId == entity.KlijentId);

            if (klijent?.Korisnik != null)
            {
                var user = klijent.Korisnik;
                var vrijemeString = entity.VrijemeTermina.ToString(@"hh\:mm");
                var emailMessage = new
                {
                    Sender = "testadna.98@gmail.com",
                    Recipient = user.Email,
                    Subject = "Novi Termin Zakazan",
                    Content = $"Poštovani {user.Ime}, vaš termin je zakazan za {entity.DatumTermina:dd.MM.yyyy} u {vrijemeString} "
                };
                _mailProducer.SendEmail(emailMessage);
            }

            return _mapper.Map<Model.Models.Termini>(entity);
        }




    }

}
