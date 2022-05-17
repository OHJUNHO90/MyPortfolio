using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameRoom
    {
		// 게임 보드판.
		List<short> gameBoard;
		// 0~49까지의 인덱스를 갖고 있는 보드판 데이터
		List<short> tableBoard;
		// 현재 턴을 진행하고 있는 플레이어의 인덱스.
		byte currentTurnPlayer;

		byte COLUMN_COUNT = 7;
		readonly short EMPTY_SLOT = short.MaxValue;

		public List<Player> players { set; get; }
		public int RoomNumber { set; get; }

		public GameRoom()
		{
			players = new List<Player>();
			gameBoard = new List<short>();
			tableBoard = new List<short>();

			for (byte i = 0; i < COLUMN_COUNT * COLUMN_COUNT; ++i)
			{
				gameBoard.Add(EMPTY_SLOT);
				tableBoard.Add(i);
			}
		}

		public void EnterGameRoom(Player player1, Player player2)
		{
			this.players.Add(player1);
			this.players.Add(player2);

			// 로딩 시작메시지 전송.
			this.players.ForEach(player =>
			{
				//플레이어들의 초기 상태를 지정해 준다.
				player.ChangeStateToEnteredRoom();
				Packet packet = new Packet((short)Message.START_LOADING);
				packet.AddBody(player.myIndex);

				/*전송 테스트*/
				player.SendMessage(packet);
			});
		}

		public bool AreYouReady()
		{
			return players.All(target => target.playerState.Equals(PLAYER_STATE.LOADING_COMPLETE));
		}

		/// <summary>
		///
		/// </summary>
		public void GameStart()
		{
			SetInitialData();

			Packet packet = new Packet((short)Message.GAME_START);
			packet.AddBody((byte)players.Count); 

			this.players.ForEach(player =>
			{	
				packet.AddBody(player.myIndex);    
				packet.AddBody((byte)player.viruses.Count);
				player.viruses.ForEach(position => packet.AddBody(position));
			});

			packet.AddBody(currentTurnPlayer);
			this.players.ForEach(player => player.SendMessage(packet));
		}

		void SetInitialData()
		{
			// 보드판 데이터 초기화.
			for (int i = 0; i < gameBoard.Count; ++i)
			{
				gameBoard[i] = EMPTY_SLOT;
			}

			// 1번 플레이어의 세균은 왼쪽위(0,0), 오른쪽위(0,6) 두군데에 배치.
			put_virus(0, 0, 0);
			put_virus(0, 0, 6);
			// 2번 플레이어는 세균은 왼쪽아래(6,0), 오른쪽아래(6,6) 두군데에 배치.
			put_virus(1, 6, 0);
			put_virus(1, 6, 6);

			// 1P부터 시작.
			currentTurnPlayer = 0;   
		}

		void put_virus(byte player_index, byte row, byte col)
		{
			short position = GetPosition(row, col);
			put_virus(player_index, position);
		}
		void put_virus(byte player_index, short position)
		{
			gameBoard[position] = player_index;
			GetPlayer(player_index).add_cell(position);
		}
		Player GetPlayer(byte targetIndex)
		{
			return this.players.Find(obj => obj.myIndex == targetIndex);
		}
		short GetPosition(byte row, byte col)
		{
			return (short)(row * COLUMN_COUNT + col);
		}

	}
}
