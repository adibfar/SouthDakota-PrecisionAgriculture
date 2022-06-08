using PAS.Application.Models.Chat;
using PAS.Application.Responses.Identity;
using PAS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using PAS.Application.Interfaces.Chat;

namespace PAS.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}