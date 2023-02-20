namespace Domain.Entities.Emails;

public class Email
{
    public int Id { get; set; }
    public string Recipient { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public int Attempts { get; set; } = 0;
}
