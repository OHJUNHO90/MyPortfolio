using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    enum PLAYER_STATE : byte
    {
        NONE,

        // 방에 막 입장한 상태.
        ENTERED_ROOM,

        // 로딩을 완료한 상태.
        LOADING_COMPLETE,

        // 턴 진행 준비 상태.
        READY_TO_TURN,

        // 턴 연출을 모두 완료한 상태.
        CLIENT_TURN_FINISHED
    }

    class Player
    {
        MessageHandler handler = null;
        public byte myIndex { get; private set; }
        public PLAYER_STATE playerState { get; set; }

        public List<short> viruses { get; private set; }

        /// <summary>
        /// // 플레이어들을 생성하고 각각 0번, 1번 인덱스를 부여해 준다.
        /// </summary>
        public Player(MessageHandler handler, byte myIndex)
        {
            this.handler = handler;
            this.myIndex = myIndex;
            viruses = new List<short>();
        }

        public string SerialNumber
        {
            private set { }
            get {
                return handler.serialNumber;
            }
        }

        public void add_cell(short position)
        {
            viruses.Add(position);
        }

        public void ChangeStateToEnteredRoom()
        {
            playerState = PLAYER_STATE.ENTERED_ROOM;
        }

        public void ChangeStateToLoadingCompleted()
        {
            playerState = PLAYER_STATE.LOADING_COMPLETE;
        }

        public void SendMessage(Packet packet)
        {
            handler.owner.SendMessage(packet);
        }

    }
}
