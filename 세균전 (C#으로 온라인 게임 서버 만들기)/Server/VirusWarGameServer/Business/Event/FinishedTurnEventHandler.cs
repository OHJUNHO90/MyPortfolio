using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class FinishedTurnEventHandler
    {
        public void Execute(GameRoom gameRoom, MessageHandler handler)
        {
            gameRoom.players.Find(target => target.SerialNumber.Equals(handler.serialNumber)).SetState(PLAYER_STATE.CLIENT_TURN_FINISHED);
            var target = gameRoom.players.Find(player => !player.playerState.Equals(PLAYER_STATE.CLIENT_TURN_FINISHED));

            if (target != null)
            {
                return;
            }

            byte body = gameRoom.currentTurnPlayer.Equals(0) ? ++gameRoom.currentTurnPlayer : --gameRoom.currentTurnPlayer;

            Packet packet = new Packet((short)Message.START_PLAYER_TURN);
            packet.AddBody(body);

            gameRoom.players.ForEach(player =>
            {
                player.SendMessage(packet);
                player.SetState(PLAYER_STATE.READY_TO_TURN);
            });
        }
    }
}
