using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Application.Abstractions.Messaging;
public interface INotificationHandler
{
    Task HandleAsync(string rawMessage);
}
