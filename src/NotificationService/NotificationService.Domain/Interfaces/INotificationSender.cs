using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Domain.Interfaces;
public interface INotificationSender
{
    Task SendAsync(Notification notification);
}
