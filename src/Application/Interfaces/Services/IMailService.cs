using PAS.Application.Requests.Mail;
using System.Threading.Tasks;

namespace PAS.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}