using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel;
public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, string topic);
}
