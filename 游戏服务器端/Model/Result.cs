using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Result
    {
        public Result(int _id,int _userID,int _totalCount,int _winCount)
        {
            id = _id;
            userID = _userID;
            totalCount = _totalCount;
            winCount = _winCount;
        }

        public int id { get; set; }
        public int userID { get; set; }
        public int totalCount { get; set; }
        public int winCount { get; set; }

    }
}
