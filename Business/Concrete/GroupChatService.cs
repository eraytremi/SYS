using Aspose.Pdf;
using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.GroupChat;
using Entity.Dtos.GroupMember;
using Entity.Dtos.GroupMessage;
using Entity.Dtos.User;
using Entity.SysModel;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupMemberRepository _memberRepository;
        private readonly IGroupMessageRepository _messageRepository;
        private readonly IGroupMessageService _messageService;
        public GroupChatService(IGroupRepository groupRepository, IUserRepository userRepository, IGroupMemberRepository memberRepository, IGroupMessageRepository messageRepository, IGroupMessageService messageService)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _memberRepository = memberRepository;
            _messageRepository = messageRepository;
            _messageService = messageService;
        }

        public async Task<ApiResponse<NoData>> AddGroupChat(PostGroupChat dto, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            if (dto.GroupId!=0)
            {
                var getGroupA = await _groupRepository.GetAllAsync(p => p.IsActive == true && p.Id==dto.GroupId);
                var listGroup = new List<GroupMember>();

                foreach (var item in dto.GroupMembers)
                {
                   
                    var mapping = new GroupMember
                    {
                        CreatedBy = currentUserId,
                        CreatedDate = DateTime.Now,
                        UserId = item.UserId,
                        IsActive = true,
                        GroupId=dto.GroupId,
                    };
                    listGroup.Add(mapping);
                    await _memberRepository.InsertAsync(mapping);
                }
            }
            else
            {
                var add = new GroupChat
                {
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    GroupName = dto.GroupName,
                };
                await _groupRepository.InsertAsync(add);

                var getGroup = await _groupRepository.GetAllAsync(p => p.IsActive == true);
                var getLastIndex = getGroup.OrderByDescending(p => p.Id).FirstOrDefault();
                var list = new List<GroupMember>();

                foreach (var item in dto.GroupMembers)
                {
                    var mapping = new GroupMember
                    {
                        CreatedBy = currentUserId,
                        CreatedDate = DateTime.Now,
                        UserId = item.UserId,
                        IsActive = true,
                        GroupId = getLastIndex.Id
                    };
                    list.Add(mapping);
                    await _memberRepository.InsertAsync(mapping);
                }
            }
            
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);

        }

        public async Task<ApiResponse<NoData>> DeleteGroupChat(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getGroup =await _groupRepository.GetByIdAsync(id);
            getGroup.DeletedDate = DateTime.Now;
            getGroup.IsActive = false;
            getGroup.DeletedBy = currentUserId;
            await _groupRepository.UpdateAsync(getGroup);


            var getMembersByGroupId =await _memberRepository.GetAllAsync(p => p.IsActive == true && p.GroupId == id);
            foreach (var member in getMembersByGroupId)
            {
                member.IsActive = false;
                member.DeletedDate = DateTime.Now;
                member.DeletedBy = currentUserId;
                await _memberRepository.UpdateAsync(member);
            }
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetGroupChat>>> GetGroupChats(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetGroupChat>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var getList =await _groupRepository.GetAllAsync(p=>p.IsActive==true);
            var list=new List<GetGroupChat>();
            foreach (var item in getList)
            {
                var listMembers= new List<GetGroupMember>(); 
                var listGroupMessages = new List<GetGroupMessage>();
                var getMembers =await _memberRepository.GetAllAsync(p=>p.GroupId==item.Id && p.IsActive==true);
                var groupMessages = await _messageRepository.GetAllAsync(p => p.GroupId == item.Id && p.IsActive == true);
                foreach (var member in getMembers)
                {
                    var user =await _userRepository.GetAsync(p => p.Id == member.UserId && p.IsActive == true);
                    var mappingUser = new GetUser
                    {
                        Id = user.Id,
                        Mail = user.Mail,
                        Name = user.Name
                    };
                    var addMember = new GetGroupMember
                    {
                        GroupId = member.Id,
                        Id = member.Id,
                        UserId = member.Id,
                        GetUser=mappingUser,
                        
                    };
                    listMembers.Add(addMember);
                }
                foreach (var groupMessage in groupMessages)
                {
                    var addList = new GetGroupMessage
                    {
                        GroupId = groupMessage.Id,
                        Id = groupMessage.Id,
                        IsSeen = groupMessage.IsSeen,
                        MessageText = groupMessage.MessageText,
                        SenderId = groupMessage.SenderId
                    };
                    listGroupMessages.Add(addList);
                }
                var add = new GetGroupChat
                {
                    GroupName = item.GroupName,
                    Id = item.Id,
                    GetGroupMembers=listMembers,
                    GetGroupMessages= listGroupMessages
                };
                list.Add(add);
            }
            return ApiResponse<List<GetGroupChat>>.Success(StatusCodes.Status200OK,list);
        }

        public async Task<ApiResponse<List<GetGroupChat>>> GetGroupChatById(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetGroupChat>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getListGroupChat = await _groupRepository.GetAllAsync(
                 p => p.IsActive == true && p.GroupMembers.Any(gm => gm.UserId == currentUserId));
            

            var list = new List<GetGroupChat>();

            var groupM = await _messageService.GetGroupMessagesByUserId(currentUserId);

            foreach (var item in getListGroupChat)
            {
                var listMembers = new List<GetGroupMember>();
                var listGroupMessages = new List<GetGroupMessage>();
                var getMembers = await _memberRepository.GetAllAsync(p => p.GroupId == item.Id && p.IsActive == true);
                var groupMessages = await _messageRepository.GetAllAsync(p => p.GroupId == item.Id && p.IsActive == true);
                foreach (var member in getMembers)
                {
                    var user = await _userRepository.GetAsync(p => p.Id == member.UserId && p.IsActive == true);
                    var mappingUser = new GetUser
                    {
                        Id = user.Id,
                        Mail = user.Mail,
                        Name = user.Name
                    };
                    var addMember = new GetGroupMember
                    {
                        GroupId = member.Id,
                        Id = member.Id,
                        UserId = member.Id,
                        GetUser = mappingUser
                    };
                    listMembers.Add(addMember);
                }
                foreach (var groupMessage in groupMessages)
                {
                    var addList = new GetGroupMessage
                    {
                        GroupId = groupMessage.Id,
                        Id = groupMessage.Id,
                        IsSeen = groupMessage.IsSeen,
                        MessageText = groupMessage.MessageText,
                        SenderId = groupMessage.SenderId
                    };
                    listGroupMessages.Add(addList);
                }
                var add = new GetGroupChat
                {
                    GroupName = item.GroupName,
                    Id = item.Id,
                    GetGroupMembers = listMembers,
                    GetGroupMessages = groupM.Data
                };
                list.Add(add);
            }
            return ApiResponse<List<GetGroupChat>>.Success(StatusCodes.Status200OK, list);

        }


        public async Task<ApiResponse<NoData>> UpdateGroupChat(UpdateGroupChat dto, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var update = new GroupChat
            {
                GroupName = dto.GroupName,
                Id = dto.Id,
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.Now,

            };
            await _groupRepository.UpdateAsync(update);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<GetGroupChat>> GetGroupChat(long groupId, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<GetGroupChat>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getMessages = await _messageRepository.GetAllAsync(p => p.IsActive == true && p.GroupId == groupId);
            var messageList = new List<GetGroupMessage>();

            foreach (var message in getMessages)
            {
                var user =await _userRepository.GetByIdAsync((long)message.SenderId);
                var mappingUserx = new GetUser
                {
                    Id = user.Id,
                    Mail =  user.Mail,
                    Name = user.Name
                };

                var add = new GetGroupMessage
                {
                    SenderId = user.Id,
                    GroupId = user.Id,
                    Id = message.Id,
                    MessageText = message.MessageText,
                    Sender = mappingUserx
                };
                messageList.Add(add);
            }

            var getMembers = await _memberRepository.GetAllAsync(p => p.IsActive == true && p.GroupId == groupId);
            var memberList = new List<GetGroupMember>();
            foreach (var getMember in getMembers)
            {
                var user =await _userRepository.GetByIdAsync(getMember.UserId);
                var mappingUserx = new GetUser
                {
                    Id = user.Id,
                    Mail = user.Mail,
                    Name = user.Name
                };
                var add = new GetGroupMember
                {
                    GroupId = getMember.Id,
                    Id = getMember.GroupId,
                    UserId = getMember.UserId,
                    GetUser = mappingUserx
                };
                memberList.Add(add);
            }
       
            var getGroupChat = await _groupRepository.GetByIdAsync(groupId);

            var mappingGroupChat = new GetGroupChat
            {
                GetGroupMessages = messageList,
                GroupName = getGroupChat.GroupName,
                Id = getGroupChat.Id,
                GetGroupMembers=memberList
            };
            return ApiResponse<GetGroupChat>.Success(StatusCodes.Status200OK,mappingGroupChat);
        }
    }
}
