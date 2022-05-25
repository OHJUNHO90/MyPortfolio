using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace VirusWarGameServer
{
    class PreventMovementMoreThanTwoCell : ConditionTesting
    {
        readonly short numberOfMoveableCell = 2;
        public PreventMovementMoreThanTwoCell(iDecorator decorator = null) : base(decorator)
        {
            
        }

        public override bool CheckLogic(params object[] datas)
        {
            return Math.Abs( (short)datas[1] - (short)datas[0] ) <= Math.Pow(numberOfMoveableCell, 4) ? true : false;
        }
    }
}
