using ChatRoom.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Data
{
    public interface IStatistics
    {
        List<SpeachStatisticsModel> SpeachRank(int count);
    }
}
