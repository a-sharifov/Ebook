namespace Infrastructure.Emails.Models;

public record SendMessageRequest(
    string To,
    string Subject,
    string Body
    );
