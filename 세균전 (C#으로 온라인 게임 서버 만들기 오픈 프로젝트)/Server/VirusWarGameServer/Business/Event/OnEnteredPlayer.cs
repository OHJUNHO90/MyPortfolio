using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class OnEnteredPlayer
    {
        public void Execute(GameRoom gameRoom, Player[] players)
        {
			gameRoom.players.Add(players[0]);
			gameRoom.players.Add(players[1]);

			// 1번 플레이어의 세균은 왼쪽위(0,0), 오른쪽위(0,6) 두군데에 배치.
			gameRoom.gameBoard.AddVirus(0, 0, players[0]);
			gameRoom.gameBoard.AddVirus(0, 6, players[0]);

			// 2번 플레이어는 세균은 왼쪽아래(6,0), 오른쪽아래(6,6) 두군데에 배치.
			gameRoom.gameBoard.AddVirus(6, 0, players[1]);
			gameRoom.gameBoard.AddVirus(6, 6, players[1]);

			// 로딩 시작 메시지 전송.
			gameRoom.players.ForEach(player =>
			{
				//플레이어들의 초기 상태를 지정해 준다.
				player.SetState(PLAYER_STATE.ENTERED_ROOM);
				Packet packet = new Packet((short)Message.START_LOADING);
				packet.AddBody(player.myIndex);

				/*전송 테스트*/
				player.SendMessage(packet);
			});
		}
    }
}
