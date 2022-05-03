
public class PacketQueue
{
    // 패킷 저장 정보.
    struct PacketInfo
    {
        public int offset;
        public int size;
    };

    private MemoryStream streamBuffer;

    private List<PacketInfo> packetList;

    private int offset = 0;


    private Object lockObj = new Object();

    public PacketQueue()
    {
        streamBuffer = new MemoryStream();
        packetList = new List<PacketInfo>();
    }

    public int Enqueue(byte[] data, int size)
    {
        PacketInfo info = new PacketInfo();

        lock (lockObj)
        {   
            info.offset = offset;
            info.size = size;

            // 패킷 저장 정보를 보존.
            packetList.Add(info);

            // 패킷 데이터를 보존.
            streamBuffer.Position = offset;
            streamBuffer.Write(data, 0, size);
            streamBuffer.Flush();

            offset += size;
        }

        return size;
    }

    public int Dequeue(ref byte[] buffer)
    {
        if (packetList.Count <= 0)
        {
            return -1;
        }

        int recvSize = 0;
        lock (lockObj)
        {
            PacketInfo info = packetList[0];
            buffer = new byte[info.size];

            streamBuffer.Position = info.offset;
            recvSize = streamBuffer.Read(buffer, 0, info.size);
            // 큐 데이터를 꺼냈으므로 선두 요소 삭제.
            packetList.RemoveAt(0);

            // 모든 큐 데이터를 꺼냈을 때는 스트림을 클리어.
            if (packetList.Count == 0)
            {
                Clear();
                offset = 0;
            }
        }

        return recvSize;
    }

    public void Clear()
    {
        byte[] buffer = streamBuffer.GetBuffer();
        Array.Clear(buffer, 0, buffer.Length);
        streamBuffer.Position = 0;
        streamBuffer.SetLength(0);
    }

    public int getPacketListCount()
    {
        return packetList.Count;
    }

}

