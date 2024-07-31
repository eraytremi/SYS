using Aspose.Pdf;
using Business.Abstract;
using DataAccess.Repositories.Abstract;
using Entity.Dtos.GroupChat;
using Entity.Dtos.GroupMember;
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
        public GroupChatService(IGroupRepository groupRepository, IUserRepository userRepository, IGroupMemberRepository memberRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _memberRepository = memberRepository;
        }

        public async Task<ApiResponse<NoData>> AddGroupChat(PostGroupChat dto, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            if (dto.GroupId!=null || dto.GroupId!=0)
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
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetGroupChat>>> GetGroupChat(long currentUserId)
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
                var getMembers =await _memberRepository.GetAllAsync(p=>p.GroupId==item.Id && p.IsActive==true);
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
                        GetUser=mappingUser
                    };
                    listMembers.Add(addMember);
                }

                var add = new GetGroupChat
                {
                    GroupName = item.GroupName,
                    Id = item.Id,
                    GetGroupMembers=listMembers
                };
                list.Add(add);
            }
            return ApiResponse<List<GetGroupChat>>.Success(StatusCodes.Status200OK,list);
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
    }
}
