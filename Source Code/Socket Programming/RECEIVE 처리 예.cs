

public void Receive()
{
	for (int i = 0; i < socketDic.Count; i++)
	{
		var socket = socketDic.ElementAt(i);
		MsgData msg = socket.Value.Receive();

		while (msg != null)
		{
			List<VCCSocketIO> sioList = new List<VCCSocketIO>();

			string msgId = string.Empty;
			msg.GetHeader<string>("MSG_ID", ref msgId);
			sioList = socketInList.FindAll(c => c.inSocketID.Equals(socket.Key) &&
															 c.in_msg_key_value.Equals(msgId) &&
															 !string.IsNullOrEmpty(c.bzMethod));
			
		   

			foreach (VCCSocketIO sio in sioList)
			{
				string className = string.Empty;
				string eventName = string.Empty;
				string[] array = sio.bzMethod.Split('.');

				if(array.Length > 1)
				{
					className = array[0];
					eventName = array[1];
				}
				else
				{
					eventName = array[0];
				}

				var eventArgs = new NodeEventArgs(eventName, msg);
				var node = GetNode(className);

				if (node != null)
				{
					node.Excute(eventArgs);
				}
				else
				{
					MainSubject.Instance.Excute(eventArgs);
				}
				
				if (eventArgs.result != null)
				{
					List<SK_IN_TO_OUT> list = sk_in_to_out_list.FindAll(t => t.SK_IN_SEQ.Equals(sio.socket_in_seq.ToString()));

					if (0 < list.Count){

						for (int j = 0; j < list.Count; j++)
						{
							List<VCCSocketIO> outEvent = socketOutList.FindAll(t => t.socket_out_seq.ToString().Equals(list[j].SK_OUT_SEQ));
							if (1 == outEvent.Count) {

								MethodInfo Info = node.GetType().GetMethod(outEvent[0].outMsgID,
									BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
								);

								Info.Invoke(node, eventArgs.data);
							}
						}  
					}
				}
			}

			sioList.Clear();
			msg = socket.Value.Receive();
		}
	}
}