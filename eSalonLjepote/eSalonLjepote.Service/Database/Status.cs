using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Service.Database
{
    public class Status
    {
        public int StatusNarudzbeId { get; set; }

        public string? Naziv { get; set; }

        public virtual ICollection<Narudzba> Narudzbas { get; set; } = new List<Narudzba>();
    }
}
