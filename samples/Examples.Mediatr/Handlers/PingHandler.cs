using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.Mediatr.Handlers;

public record Ping : IRequest<Pong>
{
    public string Message { get; init; }
}

public record Pong
{
    public string Message { get; init; }
}

public class PingHandler : IRequestHandler<Ping, Pong>
{
    public async Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return new Pong { Message = request.Message + " Pong" };
    }
}
