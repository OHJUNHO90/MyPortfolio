using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
	using FreeNet;

	public class CPlayer
	{
		CGameUser owner;
		public byte player_index { get; private set; }
		public List<short> viruses { get; private set; }

		public CPlayer(CGameUser user, byte player_index)
		{
			this.owner = user;
			this.player_index = player_index;
			this.viruses = new List<short>();
		}

		public void reset()
		{
			this.viruses.Clear();
		}

		public void add_cell(short position)
		{
			this.viruses.Add(position);
		}

		public void remove_cell(short position)
		{
			this.viruses.Remove(position);
		}

		public void send(CPacket msg)
		{
			this.owner.send(msg);
			CPacket.destroy(msg);
		}

		public void send_for_broadcast(CPacket msg)
		{
			this.owner.send(msg);
		}

		public int get_virus_count()
		{
			return this.viruses.Count;
		}
	}
}
