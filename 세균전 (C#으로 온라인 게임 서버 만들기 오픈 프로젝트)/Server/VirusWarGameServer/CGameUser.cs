using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeNet;

namespace VirusWarGameServer
{
	/// <summary>
	/// 하나의 session객체를 나타낸다.
	/// </summary>
	public class CGameUser : IPeer
	{
		CUserToken token;

		public CGameRoom battle_room { get; private set; }

		CPlayer player;

		public CGameUser(CUserToken token)
		{
			this.token = token;
			this.token.set_peer(this);
		}

		void IPeer.on_message(Const<byte[]> buffer)
		{
			// ex)
			byte[] clone = new byte[1024];
			Array.Copy(buffer.Value, clone, buffer.Value.Length);
			CPacket msg = new CPacket(clone, this);
			Program.game_main.enqueue_packet(msg, this);
		}

		void IPeer.on_removed()
		{
			Console.WriteLine("The client disconnected.");

			Program.remove_user(this);
		}

		public void send(CPacket msg)
		{
			this.token.send(msg);
		}

		void IPeer.disconnect()
		{
			this.token.socket.Disconnect(false);
		}

		void IPeer.process_user_operation(CPacket msg)
		{
			PROTOCOL protocol = (PROTOCOL)msg.pop_protocol_id();
			Console.WriteLine("protocol id " + protocol);
			switch (protocol)
			{
				case PROTOCOL.ENTER_GAME_ROOM_REQ:
					Program.game_main.matching_req(this);
					break;

				case PROTOCOL.LOADING_COMPLETED:
					this.battle_room.loading_complete(player);
					break;

				case PROTOCOL.MOVING_REQ:
					{
						short begin_pos = msg.pop_int16();
						short target_pos = msg.pop_int16();
						this.battle_room.moving_req(this.player, begin_pos, target_pos);
					}
					break;

				case PROTOCOL.TURN_FINISHED_REQ:
					this.battle_room.turn_finished(this.player);
					break;
			}
		}

		public void enter_room(CPlayer player, CGameRoom room)
		{
			this.player = player;
			this.battle_room = room;
		}
	}
}
