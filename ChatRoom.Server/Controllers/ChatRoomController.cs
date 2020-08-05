using ChatRoom.Server.Data;
using ChatRoom.Server.Model;
using ChatRoom.Server.Utils;
using ChatRoom.Server.WSService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IDatabase database;
        private readonly ChatRoomService wsService;
        public ChatRoomController(IDatabase database, ChatRoomService wsService)
        {
            this.database = database;
            this.wsService = wsService;
        }
        [HttpGet]
        public string EnterRoom(string roomId, string userId, string password)
        {
            var checkResult = false;
            var roomInfo = database.GetRoomInfo(roomId);
            if (roomInfo.UserId == userId ||
                roomInfo.Password == password)
            {
                checkResult = true;
            }
            if (checkResult == false)
            {
                return string.Empty;
            }
            else
            {
                return roomInfo.Url;
            }
        }

        [HttpGet]
        public List<ChatRoomModel> GetCurrentUserRooms()
        {
            if (this.HttpContext.Session.GetUserIsRegisted() == false)
            {
                return Enumerable.Empty<ChatRoomModel>().ToList();
            }
            var userId = this.HttpContext.Session.GetUserId();
            return database.GetRoomInfoByUser(userId);
        }

        [HttpPost]
        public ChatRoomModel CreateRoom(ChatRoomModel chatRoom)
        {
            chatRoom = database.CreateChatRoom(chatRoom);
            wsService.OpenRoom(chatRoom.Url);
            return chatRoom;
        }
    }
}
