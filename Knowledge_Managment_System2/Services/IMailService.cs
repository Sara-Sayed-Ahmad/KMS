namespace Knowledge_Managment_System2.Services
{
    public interface IMailService
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}
