using System;
using System.Collections.Generic;
using System.Text;
using NotificationService.Application.DTOs;
using NotificationService.Domain;
using NotificationService.Domain.Interfaces;

namespace NotificationService.Application;
public class NotificationAppService
{
    private readonly INotificationSender _sender;

    public NotificationAppService(INotificationSender sender)
    {
        _sender = sender;
    }

    public async Task HandleNotificationAsync(NotificationDto dto)
    {
        var notification = new Notification(dto.Type, dto.Recipient, dto.Subject, dto.Content);
        await _sender.SendAsync(notification);
    }
}
