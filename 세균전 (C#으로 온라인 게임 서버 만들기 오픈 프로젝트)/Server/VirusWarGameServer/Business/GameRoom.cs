using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
	/// <summary>
	/// TO DO: 리팩토링 대상, 너무 많은 책임과 의무를 가짐.
	/// </summary>
    public class GameRoom
    {
		public GameBoard gameBoard { private set; get; }
		public List<Player> players { private set; get; }

		RuleReferee ruleReferee;
		public int RoomNumber { set; get; }
		// 현재 턴을 진행하고 있는 플레이어의 인덱스.
		public byte currentTurnPlayer { set; get; }

		public GameRoom()
		{
			// 먼저 대기열에 합류된 유저부터 시작
			currentTurnPlayer = 0;
			players = new List<Player>();
			ruleReferee = new RuleReferee();
			gameBoard   = new GameBoard();
		}

		public bool IsReady()
		{
			return players.All(target => target.playerState.Equals(PLAYER_STATE.LOADING_COMPLETE));
		}

		public void EnterGameRoom(Player player1, Player player2)
		{
			new OnEnteredPlayer().Execute( this, new Player[] { player1, player2 } );
		}
		/// <summary>
		///
		/// </summary>
		public void GameStart()
		{
			new GameStart().Execute(this);
		}
		/// <summary>
		/// 
		/// </summary>
		public void OnMovingRequest(MessageHandler handler)
		{
			new MovingRequestEventHandler().Execute(this, handler);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="handler"></param>
		public void OnFinishedTurn(MessageHandler handler)
		{
			new FinishedTurnEventHandler().Execute(this, handler);
        }
	}
}
