using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.HubService
{
    public class ChatHub:Hub
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IHubContext<ChatHub> _context;
        public ChatHub(IUserRepository userRepository, IGroupRepository groupRepository, IHubContext<ChatHub> context)
        {

            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _context = context;
        }

        public async Task SendMessageToGroup(long groupId, string message)
        {
            var groupName = await _groupRepository.GetByIdAsync(groupId);
            // Gruba mesaj gönderme
            await Clients.Group(groupName.GroupName).SendAsync("ReceiveMessage", Context.User?.Identity?.Name, message);
        }

        public async Task JoinGroup(long groupId)
        {
            var groupName = await _groupRepository.GetByIdAsync(groupId);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName.GroupName);
            await Clients.Group(groupName.GroupName).SendAsync("ShowMessage", $"{Context.ConnectionId} gruba katıldı.");
        }

        public async Task LeaveGroup(long groupId)
        {
            var groupName = await _groupRepository.GetByIdAsync(groupId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName.GroupName);
            await Clients.Group(groupName.GroupName).SendAsync("ShowMessage", $"{Context.ConnectionId} gruptan ayrıldı.");
        }

    }
}
