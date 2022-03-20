using System.Collections.Specialized;

namespace Intel8080Emulator.Core.Registers;

public class ReadResult
{
    public BitVector32 Result { get; }

    public ReadResult(BitVector32 result)
    {
        Result = result;
    }

    public ReadResult(int value)
    {
        Result = new BitVector32(value);
    }

    public static implicit operator WritePayload(ReadResult readResult) => new(readResult.Result);
}