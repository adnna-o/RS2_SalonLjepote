using eSaljonLjepote.Services.Service;
using eSalonLjepote.Model.Request.SearchRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Service.Service
{
    public interface IStatusService:IService<Model.Models.Status, StatusNarudzbeSearchObject>
    {
    }

}
