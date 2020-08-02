using ChatRoom.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    interface IDatabase
    {
        bool SignUp(UserModel user);
        bool SignIn(UserModel user);
        bool CreateRoom(ChatRoomModel chatRoom);
        bool EnterRoom(string roomId, string userId, string password);
    }
}
