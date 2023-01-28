using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    /// <summary>
    /// 게임 종료를 감지한다.
    /// </summary>
    public class CanPlayMore
    {
        GameBoard gameBoard = null;
        public CanPlayMore(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            return false;
        }
    }
}
