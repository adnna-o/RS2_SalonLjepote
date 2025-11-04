using AutoMapper;
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
    public class NarudzbaStavkaService : BaseCRUDService<eSalonLjepote.Model.Models.NarudzbaStavka, eSalonLjepote.Service.Database.NarudzbaStavka, NarudzbaStavkeSearchObject, NarudzbaStavkaInsertRequest, NarudzbaStavkaUpdateRequest>, INarudzbaStavka
    {
        public NarudzbaStavkaService(ESalonLjepoteContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override IQueryable<NarudzbaStavka> AddFilter(IQueryable<NarudzbaStavka> query, NarudzbaStavkeSearchObject? search = null)
        {
            var filter = base.AddFilter(query, search);

            if (search?.NarudzbaId != null && search.NarudzbaId > 0)
            {
                filter = filter.Where(x => x.NarudzbaId == search.NarudzbaId);
            }
           
            return filter;
        }
    }
}
