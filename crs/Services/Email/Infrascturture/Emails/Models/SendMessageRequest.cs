namespace Infrastructure.Emails.Models;

public sealed record SendMessageRequest(
    string To,
    string Subject,
    string Body
    );
