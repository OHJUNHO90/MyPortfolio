using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameServer
    {
        object lockObj;
        Thread eventTreatmentPart;
        AutoResetEvent autoResetEventGameLoop;
        Queue<MessageHandler> packetQueue;
        GameRoomManager roomManager;

        private GameServer() { }

        private static GameServer instance = null;
        public static GameServer Instance{
            private set { }
            get
            {
                if (instance == null)
                {
                    instance = new GameServer();
                }

                return instance;
            }

        }

        public void Initialize()
        {
            lockObj = new object();
            autoResetEventGameLoop = new AutoResetEvent(false);
            packetQueue = new Queue<MessageHandler>();
            matchingWaitingUsers = new List<MessageHandler>();
            roomManager = new GameRoomManager();

            eventTreatmentPart = new Thread(GameLoop);
            eventTreatmentPart.Start();
        }

        public void EnqueuePacket(MessageHandler messageHandler)
        {
            lock (lockObj)
            {
                packetQueue.Enqueue(messageHandler);
                autoResetEventGameLoop.Set();
            }
        }

        void GameLoop()
        {
            while (true)
            {
                MessageHandler handler = null;

                lock (lockObj)
                {
                    if (packetQueue.Count > 0)
                    {
                        handler = packetQueue.Dequeue();
                    }
                }

                if (handler != null)
                {
                    ProcessUserOperation(handler);
                }

                if (packetQueue.Count <= 0)
                {
                    autoResetEventGameLoop.WaitOne();
                }
            }
        }

        void ProcessUserOperation(MessageHandler handler)
        {
            Message id = (Message)handler.packet.GetProtocolId();
            Console.WriteLine("protocol id " + id);

            switch (id)
            {
                /*방생성*/
                case Message.ENTER_GAME_ROOM_REQ:
                    {
                        ENTER_GAME_ROOM_REQ(handler);
                    }
                    break;
                case Message.LOADING_COMPLETED:
                    {
                        LOADING_COMPLETED(handler);
                    }
                    break;
                case Message.MOVING_REQ:
                    {
                        MOVING_REQ(handler);
                    }
                    break;
                case Message.TURN_FINISHED_REQ:
                    {
                        TURN_FINISHED_REQ(handler);
                    }
                    break;
            }
        }


        /// <summary>
        /// 현재 순차적으로 매칭 시킴.
        /// TO DO: 순차적인 아닌 여러 조건에 따라 좀 더 똑똑하게 매칭시키는 로직이 필요해 보임.
        /// </summary>
        /// <param name="handler"></param>
        
        List<MessageHandler> matchingWaitingUsers;
        void ENTER_GAME_ROOM_REQ(MessageHandler handler)
        {
            matchingWaitingUsers.Add(handler);

            if (2 <= matchingWaitingUsers.Count)
            {
                /*룸 생성*/
                roomManager.CreateRoom(matchingWaitingUsers[0], matchingWaitingUsers[1]);
                matchingWaitingUsers.Clear();
            }
        }

        void LOADING_COMPLETED(MessageHandler handler)
        {
            roomManager.OnLoadingCompleted(handler);
        }

        void MOVING_REQ(MessageHandler handler)
        {
            roomManager.OnMovingRequest(handler);   
        }

        void TURN_FINISHED_REQ(MessageHandler handler)
        {
            roomManager.OnTurnFinishedReq(handler);
        }
    }
}
