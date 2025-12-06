using eSalonLjepote.Model.Models;
using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Service.Database;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSalonLjepote.API.Controllers
{
    [Route("[controller]")]
    public class StatusController : BaseController<Model.Models.Status, StatusNarudzbeSearchObject>
    {
        public StatusController(ILogger<BaseController<Model.Models.Status, StatusNarudzbeSearchObject>> logger, IStatusService service) : base(logger, service)
        {
        }

    }
}
