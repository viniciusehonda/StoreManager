using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Core;

public class MediatRDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public MediatRDomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents)
    {
        foreach (EntityBase entitiesWithEvent in entitiesWithEvents)
        {
            DomainEventBase[] array = entitiesWithEvent.DomainEvents.ToArray();
            entitiesWithEvent.ClearDomainEvents();
            DomainEventBase[] array2 = array;
            foreach (DomainEventBase notification in array2)
            {
                await _mediator.Publish(notification).ConfigureAwait(continueOnCapturedContext: false);
            }
        }
    }
}
