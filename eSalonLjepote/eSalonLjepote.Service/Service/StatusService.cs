using AutoMapper;
using eSaljonLjepote.Services.Service;
using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Service.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Service.Service
{
   public class StatusService : BaseService<Model.Models.Status, Database.Status, StatusNarudzbeSearchObject>, IStatusService
    {
        public StatusService(ESalonLjepoteContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public override IQueryable<Database.Status> AddFilter(IQueryable<Database.Status> query, StatusNarudzbeSearchObject? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filteredQuery = filteredQuery.Where(x => x.Naziv.Contains(search.Naziv.ToLower()));
            }
            return filteredQuery;
        }

    }
}
