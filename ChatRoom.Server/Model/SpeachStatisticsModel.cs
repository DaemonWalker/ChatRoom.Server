using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Model
{
    public class SpeachStatisticsModel
    {
        public string UserName { get; set; }
        public int Count { get; set; }
        public int Rank { get; set; }
        public int Percent { get; set; }
    }
}
