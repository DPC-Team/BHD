using AutoMapper.QueryableExtensions;
using BHD.Application.Common.Interfaces;
using BHD.Contracts.Transacciones;
using Microsoft.EntityFrameworkCore;

namespace BHD.Application.Cuentas.Queries
{
    public record GetTransaccionesByCuentaIdQuery(int CuentaId) : IRequest<List<TransaccionDto>>;

    public class GetTransaccionesByCuentaIdQueryHandler : IRequestHandler<GetTransaccionesByCuentaIdQuery, List<TransaccionDto>>
    {
        private readonly IBHDDbContext _context;
        private readonly IMapper _mapper;

        public GetTransaccionesByCuentaIdQueryHandler(IBHDDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TransaccionDto>> Handle(GetTransaccionesByCuentaIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Transacciones
                .Where(t => t.OrigenCuentaId == request.CuentaId || t.DestinoCuentaId == request.CuentaId)
                .OrderByDescending(t => t.Fecha)
                .ProjectTo<TransaccionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
