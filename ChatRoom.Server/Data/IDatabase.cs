using ChatRoom.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    public interface IDatabase
    {
        ChatRoomModel GetRoomInfo(string roomId);
        UserModel GetUserInfo(UserModel user);
        UserModel CreateUser(UserModel user);
        ChatRoomModel CreateChatRoom(ChatRoomModel chatRoom);
    }
}
