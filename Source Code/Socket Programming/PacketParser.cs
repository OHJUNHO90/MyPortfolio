
class PacketParser
{
    private struct PacketInfo
    {
        public int size;
        public int offset;
    }

    private int headerLength;
    private PacketInfo packet;
    private byte[] paketBuffer;
    public delegate void OnCompleted(byte[] buffer, int size);

    public PacketParser(int headerLength)
    {
        this.headerLength = headerLength;
        packet = new PacketInfo();
        packet.size = headerLength;
        paketBuffer = new byte[1024];
    }

    /// <summary>
    /// 패킷 나누기
    /// </summary>
    public void Parse(byte[] buffer, int recvSize, OnCompleted callback)
    {
        byte[] result = new byte[recvSize];
        Array.Copy(buffer, 0, result, 0, recvSize);
        callback(result, recvSize);
        Clear();
    }

    /// <summary>
    /// 패킷 정보 및 패킷 버퍼 초기화
    /// </summary>
    public void Clear()
    {
        packet.offset = 0;
        packet.size = headerLength;

        Array.Clear(paketBuffer, 0, paketBuffer.Length);
    }

}