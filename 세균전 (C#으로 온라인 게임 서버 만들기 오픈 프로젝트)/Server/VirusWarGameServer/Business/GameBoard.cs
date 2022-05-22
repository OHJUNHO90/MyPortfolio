using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameBoard
    {
		List<Cell> gameBoard = new List<Cell>();
        //List<short> gameBoard = new List<short>();
		//List<short> tableBoard = new List<short>();

		byte COLUMN_COUNT = 7;
		readonly byte NO_ONE = 0;
		readonly short EMPTY_SLOT = 0;
		readonly short NOT_EMPTY = 1;
		

        public GameBoard()
        {
			for (byte i = 0; i < COLUMN_COUNT * COLUMN_COUNT; ++i)
            {
                gameBoard.Add(new Cell());
            }
		}

        public void AddVirus(short position, Player target)
        {
            gameBoard[position].PlayerIndex = target.myIndex;
            gameBoard[position].State = NOT_EMPTY;
            target.AddCell(position);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddVirus(byte row, byte col, Player target)
        {
            short position = GetPosition(row, col);
            gameBoard[position].PlayerIndex = target.myIndex;
            gameBoard[position].State = NOT_EMPTY;
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
			gameBoard[position].PlayerIndex = NO_ONE;
			gameBoard[position].State = EMPTY_SLOT;
			target.RemoveCell(position);
		}
	}
}
