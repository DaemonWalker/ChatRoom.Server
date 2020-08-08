using ChatRoom.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    public interface IDatabase : IDisposable
    {
        ChatRoomModel GetRoomInfo(string roomId);
        UserModel UserSignIn(UserModel user);
        UserModel CreateUser(UserModel user);
        ChatRoomModel CreateChatRoom(ChatRoomModel chatRoom);
        List<ChatRoomModel> GetRoomInfoByUser(string userId);
        List<UserModel> GetUsers(IEnumerable<string> ids);
    }
}
