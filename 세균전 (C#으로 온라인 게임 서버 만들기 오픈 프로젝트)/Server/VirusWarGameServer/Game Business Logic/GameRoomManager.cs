using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameRoomManager
    {
        List<GameRoom> rooms;

        public GameRoomManager()
        {
            rooms = new List<GameRoom>();
        }

        public void CreateRoom(MessageHandler user1, MessageHandler user2)
        {
            // 게임 방을 생성하여 입장 시킴.
            GameRoom battleroom = new GameRoom();
            battleroom.EnterGameRoom(new Player(user1, 0), new Player(user2, 1));

            // 방 리스트에 추가.
            this.rooms.Add(battleroom);
        }
    }
}
