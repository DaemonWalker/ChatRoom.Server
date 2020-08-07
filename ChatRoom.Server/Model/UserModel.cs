using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ChatRoom.Server.Model
{
    public class UserModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsTempUser { get; set; }
        public string Id { get; set; }
    }
}
