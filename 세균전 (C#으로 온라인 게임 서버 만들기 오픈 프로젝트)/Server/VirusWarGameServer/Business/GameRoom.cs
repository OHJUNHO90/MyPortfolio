using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public class GameRoom
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

		public bool BeReady()
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

			/*기본값 0*/
			packet.AddBody(currentTurnPlayer);
			this.players.ForEach(player => player.SendMessage(packet));
		}

		public void SetInitialData()
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

		public void OnMoveVirus(MessageHandler handler)
		{
			/* 게임 비지니스 이동 판단 로직..
			 * 서버에서 구현하는게 맞나? 의심스로움.
			 * 클라이언트에서 최초 이동 가능 여부 판단을 하는게 맞지 않나?
			 * 하지만 그러기 위해선 같은 방에 존재하는 모든 유저들의 위치 정보를 개별적으로
			 * 모든 클라이언트가 가지고 있어야 함
			 */

			byte[] body = handler.packet.GetBodyBlock();
			// 바디길이는 총 4바이트로, 처음 2바이트는 시작 위치, 그다음 2바이트는 이동 위치로 정의되어 있다.
			short current_position = BitConverter.ToInt16(body, 0);
			short target_position = BitConverter.ToInt16(body, sizeof(short));

			Player player = GetPlayer(handler.serialNumber); 

			/*동일 좌표로 이동은 불가*/


			/*목적지에 다른 세균이 위치되어있으면 이동 불가*/

			/*게임 보드판을 기준으로 1칸 이동인지 2칸이동인지 위치를 찾아*/

			/*1칸 이동이면 세균 복제
			  현재는 이동 테스트만을 목적, 게임 로직은 추후 개발*/
			put_virus(player.myIndex, target_position);

			/*2칸 이동이면 그냥 이동만*/
			//remove_virus(player.myIndex, current_position);
			//put_virus(player.myIndex, target_position);

			Packet packet = new Packet((short)Message.PLAYER_MOVED);
			this.players.ForEach(player =>
			{
				packet.AddBody(player.myIndex);
				packet.AddBody(current_position);
				packet.AddBody(target_position);
				player.SendMessage(packet);
			});
		}

		short GetPosition(byte row, byte col)
		{
			return (short)(row * COLUMN_COUNT + col);
		}
		void put_virus(byte player_index, byte row, byte col)
		{
			short position = GetPosition(row, col);
			put_virus(player_index, position);
		}
		void put_virus(byte player_index, short position)
		{
			gameBoard[position] = player_index;
			GetPlayer(player_index)?.AddCell(position);
		}
		void remove_virus(byte player_index, short position)
		{
			gameBoard[position] = EMPTY_SLOT;
			GetPlayer(player_index)?.RemoveCell(position);
		}
		Player GetPlayer(byte targetIndex)
		{
			return players.Find(obj => obj.myIndex == targetIndex);
		}

		Player GetPlayer(string serialNumber)
		{
			return players.Find(obj => obj.SerialNumber.Equals(serialNumber));
		}
	}
}
