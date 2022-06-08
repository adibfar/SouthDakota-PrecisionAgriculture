using PAS.Application.Responses.Identity;
using PAS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using PAS.Application.Interfaces.Chat;
using PAS.Application.Models.Chat;

namespace PAS.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}