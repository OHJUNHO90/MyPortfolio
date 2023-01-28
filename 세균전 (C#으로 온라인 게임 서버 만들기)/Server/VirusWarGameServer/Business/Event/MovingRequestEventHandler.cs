using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class MovingRequestEventHandler
    {
        public void Execute(GameRoom gameRoom, MessageHandler handler)
        {
			// 바디길이는 총 4바이트로, 처음 2바이트는 시작 위치, 그다음 2바이트는 이동 위치로 정의되어 있다.
			byte[] body = handler.packet.GetBodyBlock();
			short current_position = BitConverter.ToInt16(body, 0);
			short target_position = BitConverter.ToInt16(body, sizeof(short));

			ExecuteDecorator decorator = new ExecuteDecorator();
			decorator.OnCompleted = () =>
			{
				Player target = gameRoom.players.Find(obj => obj.SerialNumber.Equals(handler.serialNumber));
				gameRoom.gameBoard.RemoveVirus(current_position, target);
				gameRoom.gameBoard.AddVirus(target_position, target);

				Packet packet = new Packet((short)Message.PLAYER_MOVED);
				packet.AddBody(target.myIndex);
				packet.AddBody(current_position);
				packet.AddBody(target_position);

				gameRoom.players.ForEach(player =>
				{
					player.SendMessage(packet);
				});
			};

			decorator.Execute( new TheSamePlace( 
							   new PreventMovementMoreThanTwoCell( 
							   new DuplicateLocation())),
							   current_position,
							   target_position,
							   gameRoom.gameBoard);
		}
    }
}
