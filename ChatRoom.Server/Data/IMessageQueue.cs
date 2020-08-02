using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    interface IMessageQueue
    {
        void AddMessage(string roomId, string msg);
        void AddRoom(string roomId);
        void EnterRoom(string roomId, string userId);

    }
}
