using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NotificationService.Domain;
using NotificationService.Domain.Interfaces;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NotificationService.Infrastructure.Notifications;
public class EmailSender : INotificationSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(Notification notification)
    {
        if (!notification.Type.Equals("email", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        using var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["Email:User"]));
        email.To.Add(MailboxAddress.Parse(notification.Recipient));
        email.Subject = notification.Subject;
        email.Body = new TextPart("plain") { Text = notification.Content };

        using var smtp = new SmtpClient();
        int smtpPort = int.Parse(_config["Email:Port"] ?? "587", NumberStyles.Integer, CultureInfo.InvariantCulture);
        await smtp.ConnectAsync(_config["Email:SmtpServer"], smtpPort, false);
        await smtp.AuthenticateAsync(_config["Email:User"], _config["Email:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
