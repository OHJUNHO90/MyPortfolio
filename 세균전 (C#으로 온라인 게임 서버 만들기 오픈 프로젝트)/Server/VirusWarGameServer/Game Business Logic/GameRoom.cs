using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameRoom
    {
		List<Player> players;

		public GameRoom()
		{
			players = new List<Player>();
		}

		public void EnterGameRoom(Player player1, Player player2)
		{
			this.players.Add(player1);
			this.players.Add(player2);

			// 로딩 시작메시지 전송.
			this.players.ForEach(player =>
			{
				//플레이어들의 초기 상태를 지정해 준다.
				players.ForEach(player => player.SetEnteredRoomState());
				Packet packet = new Packet((short)Message.START_LOADING);

				/*전송 테스트*/
				player.SendMessage(packet);

				//player.enter_room(player1, this);
			});
		}
	}
}
