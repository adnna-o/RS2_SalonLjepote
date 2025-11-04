using eSaljonLjepote.Services.Service;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Service.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Service.Service
{
    public interface IKlijentiService:ICRUDService<Model.Models.Klijenti,KlijentiSearchRequest,KlijentiInsertRequest,KlijentiUpdateRequest>
    {
      //and  Klijenti GetByKorisnikId(int korisnikId);
    }
}
