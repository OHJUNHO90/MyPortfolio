using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public class GameBoard
    {
		public List<Cell> CellList { private set; get; }
        //List<short> gameBoard = new List<short>();
		//List<short> tableBoard = new List<short>();

		public readonly byte COLUMN_COUNT = 7;
        readonly byte NO_ONE = 0;
		readonly short EMPTY_SLOT = 0;
		readonly short NOT_EMPTY = 1;
		

        public GameBoard()
        {
            CellList = new List<Cell>();

            for (byte i = 0; i < Math.Pow(COLUMN_COUNT, 2); ++i) 
            {
                CellList.Add(new Cell());
            }
		}

        public void AddVirus(short position, Player target)
        {
            CellList[position].PlayerIndex = target.myIndex;
            CellList[position].State = NOT_EMPTY;
            target.AddCell(position);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddVirus(byte row, byte col, Player target)
        {
            short position = GetPosition(row, col);
            CellList[position].PlayerIndex = target.myIndex;
            CellList[position].State = NOT_EMPTY;
            target.AddCell(position);
        }

        /// <summary>
        /// 
        /// </summary>
        short GetPosition(byte row, byte col)
		{
			return (short)(row * COLUMN_COUNT + col);
		}
        /// <summary>
        /// 
        /// </summary>
        public void RemoveVirus(short position, Player target)
        {
            CellList[position].PlayerIndex = NO_ONE;
            CellList[position].State = EMPTY_SLOT;
            target.RemoveCell(position);
        }

        public bool IsItEmpty(int index)
        {
            if (index < CellList.Count)
            {
                return CellList[index].State.Equals(EMPTY_SLOT);
            }
           
            return false; 
        }
	}
}
