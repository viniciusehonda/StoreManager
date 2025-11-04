using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Application.DTOs;
public class NotificationDto
{
    public string Type { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}
