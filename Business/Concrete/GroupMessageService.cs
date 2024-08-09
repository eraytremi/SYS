using Business.Abstract;
using Business.HubService;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity.Dtos.GroupMessage;
using Entity.Dtos.User;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly IGroupMessageRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        protected IHubContext<ChatHub> _hubContext;
        public GroupMessageService(IGroupMessageRepository repository, IUserRepository userRepository, IGroupRepository groupRepository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _hubContext = hubContext;
        }

        public async Task<ApiResponse<NoData>> AddGroupMessage(PostGroupMessage message, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new GroupMessage
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                GroupId = message.GroupId,
                IsSeen = false,
                SenderId = currentUserId,
                MessageText = message.MessageText,
                IsActive = true
            };

            await _repository.InsertAsync(add);
            var groupName = await _groupRepository.GetByIdAsync((long)message.GroupId);
            //await _hubContext.Clients.Group(groupName.GroupName).SendAsync("ReceiveMessage",getUser.Name,message.MessageText);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteGMessage(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getMessage = await _repository.GetByIdAsync(id);
            getMessage.IsActive = true;
            getMessage.DeletedDate = DateTime.Now;
            getMessage.DeletedBy = currentUserId;
            await _repository.UpdateAsync(getMessage);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetGroupMessage>>> GetGroupMessagesByUserId(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetGroupMessage>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            
            var list = new List<GetGroupMessage>();
            var getList = await _repository.GetAllAsync(p => p.IsActive == true);

            foreach (var item in getList)
            {
                var user =await _userRepository.GetByIdAsync((long)item.SenderId);
                var mappingUser = new GetUser
                {
                    Id = currentUserId,
                    Mail = user.Mail,
                    Name = user.Name

                };
                var add = new GetGroupMessage
                {
                    Id = item.Id,
                    IsSeen = item.IsSeen,
                    MessageText = item.MessageText,
                    GroupId = item.GroupId,
                    SenderId = item.SenderId,
                    Sender = mappingUser

                };
                list.Add(add);
            }
            return ApiResponse<List<GetGroupMessage>>.Success(StatusCodes.Status200OK, list);
        }

        public Task<ApiResponse<NoData>> UpdateGMessage(UpdateGroupMessage message, long currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
