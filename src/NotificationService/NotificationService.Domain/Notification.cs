namespace NotificationService.Domain;

public class Notification
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Recipient { get; private set; }
    public string Subject { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Notification() { }

    public Notification(string type, string recipient, string subject, string content)
    {
        Id = Guid.NewGuid();
        Type = type;
        Recipient = recipient;
        Subject = subject;
        Content = content;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Recipient))
        {
            throw new ArgumentException("Recipient é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(Type))
        {
            throw new ArgumentException("Type é obrigatório.");
        }
    }
}

