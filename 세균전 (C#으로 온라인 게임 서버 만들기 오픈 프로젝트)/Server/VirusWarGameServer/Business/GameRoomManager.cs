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

        private GameRoom FindRoom(string serialNumber)
        {
            return rooms.Find(target => target.players.Any(target => target.SerialNumber.Equals(serialNumber)));
        }

        private Player FindPlayer(string serialNumber)
        {
            GameRoom room = FindRoom(serialNumber);
            return room.players.Find(target => target.SerialNumber.Equals(serialNumber));
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
            /*어떤방의 어떤 플레이어인지 찾아야 함.*/
            //var player = rooms.Select(target => target.players.Find(target => target.SerialNumber.Equals(handler.serialNumber)));
            //var player = room.players.Find(target => target.SerialNumber.Equals(handler.serialNumber));
            //(player as Player).ChangeStateToLoadingCompleted();

            GameRoom room = FindRoom(handler.serialNumber);
            FindPlayer(handler.serialNumber)?.ChangeStateToLoadingCompleted();

            if (!room.BeReady())
            {
                // 모든 유저가 준비 완료가 되지 않음, 대기
                return;
            }

            room.GameStart();
        }

        /// <summary>
        /// TO DO: DB에 프로토콜 아이디별 바디 정보(자료형, 길이 등)가 필요함,
        /// 현재는 하드코딩으로 가져오지만 정의된 전문 정보를 기반으로 데이터를 사용해야 함.
        /// </summary>
        public void OnMovingRequest(MessageHandler handler)
        {
            FindRoom(handler.serialNumber).OnMoveVirus(handler);
        }

    }
}
