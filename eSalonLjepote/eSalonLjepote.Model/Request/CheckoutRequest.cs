using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Model.Request
{
    public class CheckoutRequest
    {
        public int KorisnikId { get; set; }
        public string? PaypalPaymentId { get; set; }
        public DateTime? DatumNarudzbe { get; set; }
        public int? PlacanjeId { get; set; }
    }
}
