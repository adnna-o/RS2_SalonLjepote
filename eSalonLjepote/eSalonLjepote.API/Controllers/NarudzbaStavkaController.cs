using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Model;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSalonLjepote.API.Controllers
{
    [Route("[controller]")]
    public class NarudzbaStavkaController : BaseCRUDController<Model.Models.NarudzbaStavka, NarudzbaStavkeSearchObject, NarudzbaStavkaInsertRequest, NarudzbaStavkaUpdateRequest>
    {
        protected readonly INarudzbaStavka _service;
        public NarudzbaStavkaController(ILogger<BaseController<Model.Models.NarudzbaStavka, NarudzbaStavkeSearchObject>> logger, INarudzbaStavka service) : base(logger, service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

    }
}
