using AutoMapper.QueryableExtensions;
using BHD.Application.Common.Interfaces;
using BHD.Application.Services.Interfaces;
using BHD.Contracts.Cuentas;
using Microsoft.EntityFrameworkCore;

namespace BHD.Application.Services
{
    public class CuentasServices : ICuentasService
    {
        private readonly IBHDDbContext _context;
        private readonly IMapper _mapper;

        public CuentasServices(IBHDDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CuentaDto>> GetAllAsync()
        {
            return await _context.Cuentas
                .AsNoTracking()
                .ProjectTo<CuentaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

    }
}
