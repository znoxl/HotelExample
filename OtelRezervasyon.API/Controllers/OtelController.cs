using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtelRezervasyon.Domain;
using OtelRezervasyon.Application;

namespace OtelRezervasyon.API.Controllers
{
    public class OtelController : BaseApiController
    {
        private readonly IMediator _mediator;

        public OtelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Otel>>> GetOteller()
        {
            var hotels = await _mediator.Send(new List.Query());
            return Ok(hotels); // Status 200 ile döner
        }
    }
}