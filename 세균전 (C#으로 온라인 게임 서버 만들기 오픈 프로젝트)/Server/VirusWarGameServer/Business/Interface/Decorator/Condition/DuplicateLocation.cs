using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class DuplicateLocation : ConditionTesting
    {
        public DuplicateLocation(iDecorator decorator = null) : base(decorator)
        { 
            
        }

        public override bool CheckLogic(params object[] datas)
        {
            GameBoard gameBoard = datas[2] as GameBoard;
            return gameBoard.IsItEmpty((short)datas[1]);
        }
    }
}
