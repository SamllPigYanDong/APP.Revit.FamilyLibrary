using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Chat.Dto;

namespace Revit.Chat
{
    public interface IChatAppService : IApplicationService
    {
        Task<GetUserChatFriendsWithSettingsOutput> GetUserChatFriendsWithSettings();

        Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}
