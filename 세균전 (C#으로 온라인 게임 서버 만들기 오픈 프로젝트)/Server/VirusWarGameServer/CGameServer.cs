﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
	using FreeNet;
	using System.Threading;

	class CGameServer
	{
		object operation_lock;
		Queue<CPacket> user_operations;

		// 로직은 하나의 스레드로만 처리한다.
		Thread logic_thread;
		AutoResetEvent loop_event;

        //----------------------------------------------------------------
        // 게임 로직 처리 관련 변수들.
        //----------------------------------------------------------------
        // 게임방을 관리하는 매니저.
        public CGameRoomManager room_manager { get; private set; }

        // 매칭 대기 리스트.
        List<CGameUser> matching_waiting_users;
        //----------------------------------------------------------------

		public CGameServer()
		{
			this.operation_lock = new object();
			this.loop_event = new AutoResetEvent(false);
			this.user_operations = new Queue<CPacket>();

            // 게임 로직 관련.
            this.room_manager = new CGameRoomManager();
            this.matching_waiting_users = new List<CGameUser>();

			this.logic_thread = new Thread(gameloop);
			this.logic_thread.Start();
		}

		/// <summary>
		/// 게임 로직을 수행하는 루프.
		/// 유저 패킷 처리를 담당한다.
		/// </summary>
		void gameloop()
		{
			while (true)
			{
				CPacket packet = null;
				lock (this.operation_lock)
				{
					if (this.user_operations.Count > 0)
					{
						packet = this.user_operations.Dequeue();
					}
				}

				if (packet != null)
				{
					// 패킷 처리.
					process_receive(packet);
				}

				// 더이상 처리할 패킷이 없으면 스레드 대기.
				if (this.user_operations.Count <= 0)
				{
					this.loop_event.WaitOne();
				}
			}
		}

		public void enqueue_packet(CPacket packet, CGameUser user)
		{
			lock (this.operation_lock)
			{
				this.user_operations.Enqueue(packet);
				this.loop_event.Set();
			}
		}

		void process_receive(CPacket msg)
		{
			//todo:
			// user msg filter 체크.

			msg.owner.process_user_operation(msg);
		}


        /// <summary>
        /// 유저로부터 매칭 요청이 왔을 때 호출됨.
        /// </summary>
        /// <param name="user">매칭을 신청한 유저 객체</param>
        public void matching_req(CGameUser user)
        {
            // 대기 리스트에 중복 추가 되지 않도록 체크.
			if (this.matching_waiting_users.Contains(user))
			{
				return;
			}

            // 매칭 대기 리스트에 추가.
            this.matching_waiting_users.Add(user);

            // 2명이 모이면 매칭 성공.
            if (this.matching_waiting_users.Count == 2)
            {
                // 게임 방 생성.
                this.room_manager.create_room(this.matching_waiting_users[0], this.matching_waiting_users[1]);

                // 매칭 대기 리스트 삭제.
                this.matching_waiting_users.Clear();
            }
        }


		public void user_disconnected(CGameUser user)
		{
			if (this.matching_waiting_users.Contains(user))
			{
				this.matching_waiting_users.Remove(user);
			}
		}
	}
}
