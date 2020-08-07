using ChatRoom.Server.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    public class MySqlDatabase : IDatabase
    {
        #region sql
        const string INSERT_CHATROOM = @"
INSERT INTO chatroom ( NAME, UserId, IsPublic, PASSWORD, Url )
VALUES
	(
		'{0}',
		'{1}',
		{2},
	'{3}',
	'{4}')";

        const string INSERT_USER = @"
INSERT User ( Account, PASSWORD, NAME, LEVEL )
VALUES
	(
		'{0}',
		'{1}',
	'{2}',
	1)";

        const string COUNT_CHATROOM = @"
SELECT
	count( 1 ) 
FROM
	chatroom t 
WHERE
	t.NAME = '{0}'";

        const string COUNT_USER = @"
SELECT
	count( 1 ) 
FROM
	User t 
WHERE
	t.Account = '{0}' 
	AND t.NAME = '{1}'";

        const string QUERY_USER_ACCOUNT = @"
SELECT
	* 
FROM
	User t 
WHERE
	t.account = '{0}'";

        const string QUERY_USER_ID = @"
SELECT
	* 
FROM
	User t 
WHERE
	t.Id = '{0}'";

        const string QUERY_CHATROOM_NAME = @"
SELECT
	* 
FROM
	chatroom t 
WHERE
	t.NAME = '{0}'";

        const string QUERY_CHATROOM_UserId = @"
SELECT
	* 
FROM
	chatroom t 
WHERE
	t.UserId = '{0}'";
        #endregion

        private readonly MySqlConnection conn;
        private bool disposed = false;
        public MySqlDatabase(IConfiguration config)
        {
            var section = config.GetSection("Mysql");
            conn = new MySqlConnection($"Server={section["Server"]};Database={section["Database"]};User={section["User"]};Password={section["Password"]}");
            conn.Open();
        }

        public ChatRoomModel CreateChatRoom(ChatRoomModel chatRoom)
        {
            var count = conn.QueryFirst<int>(string.Format(COUNT_CHATROOM, chatRoom.Name));
            if (count == 0)
            {
                conn.Execute(string.Format(
                    INSERT_CHATROOM,
                    chatRoom.Name, chatRoom.UserId, chatRoom.IsPublic ? 1 : 0, chatRoom.Password, Guid.NewGuid().ToString("N")));

                return conn.QueryFirst<ChatRoomModel>(string.Format(QUERY_CHATROOM_NAME, chatRoom.Name));
            }
            else
            {
                return default;
            }
        }

        public UserModel CreateUser(UserModel user)
        {
            var count = conn.QueryFirst<int>(string.Format(COUNT_USER, user.Account, user.Name));
            if (count == 0)
            {
                conn.Execute(string.Format(
                    INSERT_USER,
                    user.Account, user.Name, user.Password, 1));
                return conn.QueryFirst<UserModel>(string.Format(QUERY_USER_ACCOUNT, user.Account));
            }
            else
            {
                return default;
            }
        }

        public ChatRoomModel GetRoomInfo(string roomId)
        {
            return conn.QueryFirst<ChatRoomModel>(string.Format(QUERY_USER_ID, roomId));
        }

        public List<ChatRoomModel> GetRoomInfoByUser(string userId)
        {
            return conn.Query<ChatRoomModel>(string.Format(QUERY_CHATROOM_UserId, userId)).ToList();
        }

        public UserModel UserSignIn(UserModel user)
        {
            var model = conn.QueryFirst<UserModel>(string.Format(QUERY_USER_ACCOUNT, user.Account));
            if (model.Password == user.Password)
            {
                return model;
            }
            else
            {
                return default;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool dispoing)
        {
            if (disposed == false)
            {
                this.disposed = true;
                if (dispoing)
                {
                    this.conn.Close();
                }
            }
        }
    }
}
