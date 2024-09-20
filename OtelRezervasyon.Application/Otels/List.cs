using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtelRezervasyon.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Persistence;

namespace OtelRezervasyon.Application
{
    public class List
    {
        public class Query : IRequest<List<Otel>> { }

        public class Handler : IRequestHandler<Query, List<Otel>>
        {
            private readonly OtelRezervasyonDbContext _context;
            public Handler(OtelRezervasyonDbContext context)
            {
                _context = context;
            }
            public async Task<List<Otel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Oteller.ToListAsync();
            }
        }
    }
}