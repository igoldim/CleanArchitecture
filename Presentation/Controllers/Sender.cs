using Application.Webinars.Commands.CreateWebinar;
using Application.Webinars.Queries.GetWebinarById;
using Domain.Entities;

namespace Presentation.Controllers
{
    internal class Sender
    {
        internal static Task<Webinar> Send(GetWebinarByIdQuery o, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Webinar(Guid.NewGuid(), "Teste", DateTime.Now));
        }

        internal static Task<Guid> Send(CreateWebinarCommand o, CancellationToken cancellationToken)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}