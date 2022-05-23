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
		// 현재 턴을 진행하고 있는 플레이어의 인덱스.
		byte currentTurnPlayer;

		public List<Player> players { set; get; }
		public int RoomNumber { set; get; }

		RuleReferee ruleReferee;
		GameBoard gameBoard;

		public GameRoom()
		{
			// 먼저 대기열에 합류된 유저부터 시작
			currentTurnPlayer = 0;
			players = new List<Player>();
			ruleReferee = new RuleReferee();
			gameBoard   = new GameBoard();
		}

		Player GetPlayer(byte targetIndex)
		{
			return players.Find(obj => obj.myIndex == targetIndex);
		}
		Player GetPlayer(string serialNumber)
		{
			return players.Find(obj => obj.SerialNumber.Equals(serialNumber));
		}

		public void EnterGameRoom(Player player1, Player player2)
		{
			players.Add(player1);
			players.Add(player2);

			// 1번 플레이어의 세균은 왼쪽위(0,0), 오른쪽위(0,6) 두군데에 배치.
			gameBoard.AddVirus(0, 0, player1);
			gameBoard.AddVirus(0, 6, player1);
			// 2번 플레이어는 세균은 왼쪽아래(6,0), 오른쪽아래(6,6) 두군데에 배치.
			gameBoard.AddVirus(6, 0, player2);
			gameBoard.AddVirus(6, 6, player2);

			// 로딩 시작 메시지 전송.
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

		public bool IsReady()
		{
			return players.All(target => target.playerState.Equals(PLAYER_STATE.LOADING_COMPLETE));
		}

		/// <summary>
		///
		/// </summary>
		public void GameStart()
		{
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

		/// <summary>
		/// 
		/// </summary>
		public void OnMovingRequest(MessageHandler handler)
		{
			byte[] body = handler.packet.GetBodyBlock();
			// 바디길이는 총 4바이트로, 처음 2바이트는 시작 위치, 그다음 2바이트는 이동 위치로 정의되어 있다.
			short current_position = BitConverter.ToInt16(body, 0);
			short target_position  = BitConverter.ToInt16(body, sizeof(short));

			/*데코레이터로 조건 검사 수행*/
			/*1. 동일 좌표 이동 불가*/
			/*2. 2칸이상 이동 불가*/
			/*3. 이미 바이러스가 존재하는 셀로는 이동이 불가.*/

			/*TO DO: 단위테스트 진행*/
			ExecuteDecorator decorator = new ExecuteDecorator();
			bool executionResult = decorator.Execute( new TheSamePlace( new PreventMovementMoreThanTwoCell( new DuplicateLocation() ) ),
													  current_position,
													  target_position, 
													  this.gameBoard );

			/* TO DO: 모든 조건을 통과시, 함수포인터로 실행되는 방식으로 수정*/
			if (executionResult)
			{
				Player player = GetPlayer(handler.serialNumber);
				gameBoard.RemoveVirus(current_position, player);
				gameBoard.AddVirus(target_position, player);

				Packet packet = new Packet((short)Message.PLAYER_MOVED);

				this.players.ForEach(player =>
				{
					packet.AddBody(player.myIndex);
					packet.AddBody(current_position);
					packet.AddBody(target_position);
					player.SendMessage(packet);
				});
			}

		}
	}
}
