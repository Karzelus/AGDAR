namespace AGDAR.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subjcet, string message);
    }
}
