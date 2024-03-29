﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    /// <summary>
    /// 게임 규칙에 따른 다른 행동을 위한 클래스
    /// </summary>
    class RuleReferee
    {
        CanPlayMore canPlayMore = null;
        GameRoom gameRoom       = null;
        public RuleReferee(GameRoom gameRoom)
        {
            this.gameRoom = gameRoom;
            canPlayMore = new CanPlayMore(gameRoom.gameBoard);
        }
        public bool CanPlayMore()
        {
            return canPlayMore.Execute();
        }
    }
}
