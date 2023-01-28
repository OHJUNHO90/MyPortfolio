using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public enum PLAYER_STATE : byte
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

    public class Player
    {
        MessageHandler handler = null;
        public byte myIndex { get; private set; }
        public PLAYER_STATE playerState { get; set; }
        public List<short> viruses { get; private set; }

        private PlayerAction myAction;


        /// <summary>
        /// 
        /// </summary>
        public Player()
        {
            this.handler = null;
            this.myIndex = 0;
            viruses = new List<short>();
        }

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

        public void AddCell(short position)
        {
            viruses.Add(position);
        }

        public void RemoveCell(short position)
        {
            this.viruses.Remove(position);
        }

        public void SetState(PLAYER_STATE state)
        {
            playerState = state;
        }

        public void SendMessage(Packet packet)
        {
            handler.owner.SendMessage(packet);
        }


    }
}
