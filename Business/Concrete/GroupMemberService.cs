using Business.Abstract;
using DataAccess.Repositories.Abstract;
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
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IGroupMemberRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        public GroupMemberService(IGroupMemberRepository repository, IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<ApiResponse<NoData>> AddAsync(PostGroupMember groupMember, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var add = new GroupMember
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                UserId = groupMember.UserId,
                IsActive = true
            };
            await _repository.InsertAsync(add);
            return ApiResponse<NoData>.Success(StatusCodes.Status201Created);
        }

        public async Task<ApiResponse<NoData>> DeleteAsync(long id,long groupId, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }
            var getGroupChat = await _groupRepository.GetByIdAsync(groupId);
            var getMember = getGroupChat.GroupMembers.SingleOrDefault(p => p.Id == id);
            getMember.DeletedDate = DateTime.Now;
            getMember.DeletedBy = currentUserId;
            getMember.IsActive = false;
            await _repository.DeleteAsync(getMember);
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<List<GetGroupMember>>> GetAsync(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetGroupMember>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getList =await _repository.GetAllAsync(p=>p.IsActive==true);
            var list = new List<GetGroupMember>();

            foreach (var item in getList)
            {
                var add = new GetGroupMember
                {
                    GroupId = item.Id,
                    Id = item.Id,
                    UserId = item.UserId,
                };
                list.Add(add);
            }

            return ApiResponse<List<GetGroupMember>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<List<GetGroupMember>>> GetMembersByGroupIdAsync(long id, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<GetGroupMember>>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var getMembers = await _repository.GetAllAsync(p => p.GroupId == id && p.IsActive == true);
            var list = new List<GetGroupMember>();

            foreach (var member in getMembers)
            {
                var getUserById=await _userRepository.GetByIdAsync(member.UserId);
                if (getUserById!=null)
                    
                {
                    var user = new GetUser
                    {
                        Id = getUserById.Id,
                        Name = getUserById.Name,
                        Mail = getUserById.Mail,
                    };
                    var groupMember = new GetGroupMember
                    {
                        Id = member.Id,
                        GetUser=user,
                        GroupId = member.GroupId,
                        UserId=member.UserId
                    };
                    list.Add(groupMember);
                }
            }
            return ApiResponse<List<GetGroupMember>>.Success(StatusCodes.Status200OK, list);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(UpdateGroupMember groupMember, long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Yetki yok");
            }

            var update = new GroupMember
            {
                GroupId = groupMember.GroupId,
                UpdatedBy = currentUserId,
                UpdatedDate = DateTime.UtcNow,
                UserId=groupMember.UserId,
            };
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
