using ChatRoom.Server.Data;
using ChatRoom.Server.Model;
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
            if (roomInfo.OwnerUserId == userId ||
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
                return roomInfo.Link;
            }
        }

        [HttpPost]
        public ChatRoomModel CreateRoom(ChatRoomModel chatRoom)
        {
            chatRoom = database.CreateChatRoom(chatRoom);
            wsService.OpenRoom(chatRoom.Link);
            return chatRoom;
        }
    }
}
