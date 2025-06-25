using BHD.Application.Services.Interfaces;
using BHD.Contracts.Transacciones;

namespace BHD.Application.Transacciones.Commands
{
    public record CreateTransferenciaCommand(TransferenciaDto Transferencia) : IRequest;

    public class CreateTransferenciaCommandHandler : IRequestHandler<CreateTransferenciaCommand>
    {
        private readonly ITransaccionesService _service;

        public CreateTransferenciaCommandHandler(ITransaccionesService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(CreateTransferenciaCommand request, CancellationToken cancellationToken)
        {
            await _service.TransferirAsync(request.Transferencia);
            return Unit.Value;
        }
    }
}
