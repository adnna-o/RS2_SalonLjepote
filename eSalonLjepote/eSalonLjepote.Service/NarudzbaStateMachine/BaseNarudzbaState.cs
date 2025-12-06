using AutoMapper;
using eSalonLjepote.Model;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Service.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSalonLjepote.Service.NarudzbaStateMachine
{
    public class BaseNarudzbaState
    {
        public ESalonLjepoteContext _context { get; set; }
        public IMapper _mapper { get; set; }
        private readonly IServiceProvider _serviceProvider;

        public BaseNarudzbaState(ESalonLjepoteContext context, IMapper mapper, IServiceProvider serviceProvider)
        {
            _context = context;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public virtual Task<Model.Models.Narudzba> Insert(NarudzbaInsertRequest request)
        {
            throw new UserException("Not allowed");
        }

        public virtual Task<Model.Models.Narudzba> Update(int id, NarudzbaUpdateRequest request)
        {
            throw new UserException("Not allowed");
        }

        public virtual Task<Model.Models.Narudzba> Activate(int id)
        {
            throw new UserException("Not allowed");
        }

        public virtual Task<Model.Models.Narudzba> Hide(int id)
        {
            throw new UserException("Not allowed");
        }

        public virtual Task<Model.Models.Narudzba> Delete(int id)
        {
            throw new UserException("Not allowed");
        }

        public BaseNarudzbaState CreateState(string stateName)
        {
            Console.WriteLine($"Attempting to create state with name: {stateName}");

            switch (stateName.ToLower()) // Koristimo .ToLower() kako bi upoređivanje bilo case-insensitive
            {
                case "initial":
                    return (BaseNarudzbaState)_serviceProvider.GetService(typeof(InitialNarudzbaState));
                case "draft":
                    return (BaseNarudzbaState)_serviceProvider.GetService(typeof(DraftNarudzbaState));
                case "active":
                    return (BaseNarudzbaState)_serviceProvider.GetService(typeof(ActiveNarudzbaState));
                default:
                    throw new Exception("State not recognized");
            }
        }

        public virtual async Task<List<string>> AllowedActions()
        {
            return new List<string>();
        }
    }

}
