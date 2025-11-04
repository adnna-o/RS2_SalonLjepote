using eSalonLjepote.Model.Request.SearchRequest;
using eSalonLjepote.Model.Request;
using eSalonLjepote.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eSalonLjepote.Service.RabbitMQ;
using eSalonLjepote.Model.Models;

namespace eSalonLjepote.API.Controllers
{
    [Route("[controller]")]
    
    public class TerminiController : BaseCRUDController<Model.Models.Termini, TerminiSearchRequest, TerminiInsertRequest, TerminiUpdateRequest>
    {
        private readonly ITerminiService _service;
        public readonly IMailProducer _mailProducer;
        public TerminiController(IMailProducer mailProducer, ILogger<BaseController<Model.Models.Termini, TerminiSearchRequest>> logger, ITerminiService service) : base(logger, service)
        {
            _service=service;
            _mailProducer=mailProducer;
        }
        public override Termini Insert([FromBody] TerminiInsertRequest insert)
        {
            return base.Insert(insert);
        }

        public class EmailModel
        {
            public string Sender { get; set; }
            public string Recipient { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }
        }

    }
}
