using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSaljonLjepote.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSalonLjepote.Model.Models;
using eSalonLjepote.Service.Database;

namespace eSalonLjepote.Service.Service
{
    public interface INarudzbaStavka : ICRUDService<eSalonLjepote.Model.Models.NarudzbaStavka, NarudzbaStavkeSearchObject, NarudzbaStavkaInsertRequest, NarudzbaStavkaUpdateRequest>
    {

      
    }
}
