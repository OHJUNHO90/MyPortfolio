using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameStart
    {
        public void Execute(GameRoom gameRoom)
        {
			Packet packet = new Packet((short)Message.GAME_START);
			packet.AddBody((byte)gameRoom.players.Count);

			gameRoom.players.ForEach(player =>
			{
				packet.AddBody(player.myIndex);
				packet.AddBody((byte)player.viruses.Count);
				player.viruses.ForEach(position => packet.AddBody(position));
			});

			/*기본값 0*/
			packet.AddBody(gameRoom.currentTurnPlayer);
			gameRoom.players.ForEach(player => player.SendMessage(packet));
		}
    }
}
