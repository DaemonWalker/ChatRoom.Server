using ChatRoom.Server.Data;
using ChatRoom.Server.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatistics statistics;
        public StatisticsController(IStatistics statistics)
        {
            this.statistics = statistics;
        }

        [HttpPost]
        public List<SpeachStatisticsModel> SpeachRank([FromBody]int count)
        {
            count = Math.Min(count, 100);
            var data=statistics.SpeachRank(count);
            data.ForEach(p => p.Percent = p.Count * 100 / data.First().Count);

            return data;
        }
    }
}
