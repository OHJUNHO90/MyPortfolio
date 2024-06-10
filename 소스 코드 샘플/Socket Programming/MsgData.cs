
using System;
using System.Collections.Generic;

public class MsgData
{
	Dictionary<string, object> Body   = new Dictionary<string, object>();
	Dictionary<string, object> Header = new Dictionary<string, object>();
	
	int headerLength = -1;
	int bodyLength = -1;
	
	public MsgData(int headerLength, int bodyLength)
	{
		this.headerLength = headerLength;
		this.bodyLength = bodyLength;
	}
	
	public object GetBody()
	{
		return Body;
	}

	public T GetBody<T>(string key)
	{
		if (Body.ContainsKey(key))
		{
			return (T)Body[key];
		}

		return default(T);
	}

	public void GetBody<T>(string key, ref T value)
	{
		if (Body.ContainsKey(key))
		{
			value = (T)Body[key];
		}
	}

	public void GetHeader<T>(string key, ref T value)
	{
		if (Header.ContainsKey(key))
		{
			value = (T)Header[key];
		}
	}
	
	public void SetHeader(byte[] buffer, Dictionary<string, VCCSocket.HeaderInfo> info)
	{
		int offset = 0;
		foreach (KeyValuePair<string, VCCSocket.HeaderInfo> pair in info)
		{
			byte[] header = new byte[pair.Value.length];
			Array.Copy(buffer, offset, header, 0, pair.Value.length);

			int index = 0;
			byte[] temp = new byte[pair.Value.length];
			
			for (int i = offset; i < (offset + pair.Value.length); i++)
			{
				temp[index] = buffer[i];
				index++;
			}

			offset += pair.Value.length;

			switch (pair.Value.type)
			{
				case "STRING":
					{
						Header.Add(pair.Value.dt_id, Encoding.UTF8.GetString(temp));
					}
					break;
				case "INT":
					{
						int type = BitConverter.ToInt32(temp, 0);
						Header.Add(pair.Value.dt_id, type);
					}
					break;
				case "SHORT":
					{
						short type = BitConverter.ToInt16(temp, 0);
						Header.Add(pair.Value.dt_id, type);
					}
					break;
				case "BYTE":
					{
						Header.Add(pair.Value.dt_id, temp);
					}
					break;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void SetBody(byte[] buffer, int msgLength)
	{

		List<MsgbodyFormat> list = GetMessageStructure();

		if (list == null) {
			return null;
		}

		int index = headerLength;
		for (int i = 0; i < list.Count; i++)
		{
			byte[] temp = new byte[list[i].VAL_LEN];
			Array.Copy(buffer, index, temp, 0, list[i].VAL_LEN);

			if (!list[i].VAL_TYPE.Equals("STRING"))
			{
				if (BitConverter.IsLittleEndian){
					Array.Reverse(temp);
				}
			}

			switch (list[i].VAL_TYPE)
			{
				case "STRING":
					{
						string str = Encoding.UTF8.GetString(temp).PadRight(list[i].VAL_LEN,' ');
						Body.Add(list[i].VAL_ID, str);
					}
					break;
				case "INT":
					{
						int _temp = BitConverter.ToInt32(temp, 0);
						Body.Add(list[i].VAL_ID, _temp);
					}
					break;
				case "FLOAT":
					{
						float _temp = BitConverter.ToSingle(temp, 0);
						Body.Add(list[i].VAL_ID, _temp);
					}
					break;
			}

			index += (list[i].VAL_LEN);
		}
	}

	/// <summary>
	///
	/// </summary>
	public void VerifyReceivedMsgLength(int bufferSize){

		List<MsgbodyFormat> msg = GetMessageStructure();

		if(msg == null)
		{
			return null;
		}

		int messageLength = headerLength;

		for(int i=0; i < msg.length; i++)
		{
			messageLength += list[i].VAL_LEN;
		}

		if(bufferSize < messageLength){
			Debug.Log("Packet length is not long enough." + msgId, logType.TcpIP);
			return null;
		}
	}


	public List<MsgbodyFormat> GetMessageStructure(){

		string msgId = string.Empty;
		GetHeader<string>("MSG_ID", ref msgId);

		List<MsgbodyFormat> list = VCCSocketManager.Instance().MessageDictionary(msgId);

		if (list == null) {
			Debug.Log("MESSAGE BODY IS NOT DEFINED" + msgId, logType.TcpIP);
			return null;
		}

		return list;
	}
}