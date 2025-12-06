using System;
using System.Collections.Generic;

namespace eSalonLjepote.Service.Database;

public partial class Narudzba
{
    public int NarudzbaId { get; set; }

    public int? ProizvodId { get; set; }

    public int? KorisnikId { get; set; }

    public int? PlacanjeId { get; set; }

    public DateTime? DatumNarudzbe { get; set; }

    public int? KolicinaProizvoda { get; set; }

    public decimal? IznosNarudzbe { get; set; }
    public string? PaymentId { get; set; }
    public string? StateMachine { get; set; }
    public int? StatusNarudzbeId { get; set; }
    public virtual Status? StatusNarudzbe { get; set; }

    public virtual Korisnik? Korisnik { get; set; } 

    public virtual Placanje? Placanje { get; set; } 

    public virtual Proizvod? Proizvod { get; set; }

    public virtual ICollection<Klijenti> Klijentis { get; } = new List<Klijenti>();
    public virtual ICollection<NarudzbaStavka> Stavke { get; set; } = new List<NarudzbaStavka>();

}
