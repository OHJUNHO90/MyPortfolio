using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameRoomManager
    {
        List<GameRoom> rooms;
        private int roomNumber;
        public GameRoomManager()
        {
            rooms = new List<GameRoom>();
            roomNumber = 1;
        }

        public void CreateRoom(MessageHandler handler1, MessageHandler handler2)
        {
            //TO DO: int 자료형의 값의 범위를 넘었을때를 대비한 예외가 필요 함.
            Interlocked.Increment(ref roomNumber);

            GameRoom battleroom = new GameRoom();
            battleroom.RoomNumber = roomNumber;

            battleroom.EnterGameRoom(new Player(handler1, 0), new Player(handler2, 1));

            // 방 리스트에 추가.
            this.rooms.Add(battleroom);
        }

        public void OnLoadingCompleted(MessageHandler handler)
        {
            GameRoom room = rooms.Find(target => target.players.Any(target => target.SerialNumber.Equals(handler.serialNumber)));

            /*어떤방의 어떤 플레이어인지 찾아야 함.*/
            var player = rooms.Select(target => target.players.Find(target => target.SerialNumber.Equals(handler.serialNumber)));

            foreach (var item in player)
            {
                (item as Player).ChangeStateToLoadingCompleted();
            }

            if (!room.AreYouReady())
            {
                // 모든 유저가 준비 완료가 되지 않음, 대기
                return;
            }

            room.GameStart();

        }
    }
}
