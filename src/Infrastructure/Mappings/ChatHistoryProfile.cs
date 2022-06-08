using AutoMapper;
using PAS.Application.Interfaces.Chat;
using PAS.Application.Models.Chat;
using PAS.Infrastructure.Models.Identity;

namespace PAS.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<User>>().ReverseMap();
        }
    }
}