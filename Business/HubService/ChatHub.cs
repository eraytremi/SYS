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

        public ChatHub(IUserRepository userRepository, IGroupRepository groupRepository)
        {

            _userRepository = userRepository;
            _groupRepository = groupRepository; 
        }

        public async Task SendMessageToGroup(long groupId, string message,long currentUserId)
        {
            var groupName = await _groupRepository.GetByIdAsync(groupId);
            // Gruba mesaj gönderme
            await Clients.Group(groupName.GroupName).SendAsync("ReceiveMessage", currentUserId, message);
        }

        public async Task JoinGroup(string connectionId,long groupId)
        {
            var groupName = await _groupRepository.GetByIdAsync(groupId);
           
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName.GroupName);
            await Clients.Group(groupName.GroupName).SendAsync("ShowMessage", $"{Context.ConnectionId} gruba katıldı.");
        }

            
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);
        }

    }
}
