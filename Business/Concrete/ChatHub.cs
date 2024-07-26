using Business.Abstract;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Entity.Dtos.Message;
using Entity.SysModel;
using Infrastructure.Exceptions;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Business.Concrete
{
    public class ChatHub : Hub, IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        protected IHubContext<ChatHub> _hubContext;
        public ChatHub(IChatRepository chatRepository, IUserRepository userRepository, IHubContext<ChatHub> hubContext)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        public async Task<ApiResponse<List<Message>>> GetMessages(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<Message>>.Fail(StatusCodes.Status400BadRequest, "Kullanıcı bulunamadı");
            }
            var getChats = await _chatRepository.GetAllAsync(p=>p.IsActive==true);
            return ApiResponse<List<Message>>.Success(StatusCodes.Status200OK,getChats);
        }

        public async Task<ApiResponse<List<UnreadMessage>>> GetUnreadMessages(long currentUserId)
        {
            var getUser = await _userRepository.GetByIdAsync(currentUserId);
            if (getUser == null)
            {
                return ApiResponse<List<UnreadMessage>>.Fail(StatusCodes.Status400BadRequest, "Kullanıcı bulunamadı");
            }
            var getChats = await _chatRepository.GetAllAsync(p => p.IsActive == true && p.IsSeen==false);
            var unreadMessages =new List<UnreadMessage>();
            foreach (var chat in getChats)
            {
             
                var add = new UnreadMessage
                {
                    MessageText = chat.MessageText,
                    Date = chat.Date,
                    Reciever=chat.Reciever,
                    Sender=chat.Sender,
                };
                unreadMessages.Add(add);
            }
            return ApiResponse<List<UnreadMessage>>.Success(StatusCodes.Status200OK, unreadMessages);
        }

        //ReceiveMessage:Client Method  , Server Method:SendMessage
        //await Clients.All.SendAsync("ReceiveMessage", user, message); All deyince tüm clientlar hub'a abone olmak zorundadır. 
        public async Task SendMessage(string user, string message)
        {
            //var getUser = await _userRepository.GetByIdAsync(currentUserId);
            //if (getUser == null)
            //{
            //    return new BadRequestException("kullanıcı doğrulanamadı.");
            //}
            //if (Clients == null)
            //{
            //    throw new NullReferenceException("Clients is null.");
            //}
            var messageObj = new Message
            {
                UserName = user,
                MessageText = message,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            await _chatRepository.InsertAsync(messageObj);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        }

       
    }
}

