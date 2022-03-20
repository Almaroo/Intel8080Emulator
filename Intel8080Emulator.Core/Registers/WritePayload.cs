using System.Collections.Specialized;

namespace Intel8080Emulator.Core.Registers;

public class WritePayload
{
    public BitVector32 Payload { get; }
    public WritePayload(BitVector32 value)
    {
        Payload = value;
    }

    public WritePayload(int value)
    {
        Payload = new BitVector32(value);
    }

    public static implicit operator ReadResult(WritePayload writePayload) => new(writePayload.Payload);
}

