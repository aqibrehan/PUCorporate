using PUCorporate.Model.Model;

namespace PUCorporate.DataAccessLayer.Services.Interfaces
{
    public interface IMail
    {
        Task<string> Sendmail(MailModel model, string email, string password);
        Task<string> SendForgotPasswordEmail(MailModel model, string recipientemail, string resetLink);
    }
}
