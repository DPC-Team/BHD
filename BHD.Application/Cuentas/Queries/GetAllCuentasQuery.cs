using BHD.Application.Services.Interfaces;
using BHD.Contracts.Cuentas;

namespace BHD.Application.Cuentas.Queries
{
    public record GetAllCuentasQuery() : IRequest<IEnumerable<CuentaDto>>;

    public class GetAllCuentasQueryHandler : IRequestHandler<GetAllCuentasQuery, IEnumerable<CuentaDto>>
    {
        private readonly ICuentasService _service;

        public GetAllCuentasQueryHandler(ICuentasService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CuentaDto>> Handle(GetAllCuentasQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync();
        }
    }
}
