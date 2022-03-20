using System.Collections.Specialized;
using Intel8080Emulator.Core.Enums;

namespace Intel8080Emulator.Core.Registers;

internal class CombinedRegister : RegisterBase<I16BitRegister>
{
    private Register Low { get; }
    private Register High { get; }

    internal CombinedRegister(RegisterName name, Register low, Register high) : base(name)
    {
        Low = low;
        High = high;
    }

    public override void Write(WritePayload value)
    {
        if(value.Payload.Data > ushort.MaxValue)
            throw new ArgumentException(
                $"Payload exceeded {I16BitRegister.Size}bit size, make sure to use {I16BitRegister.ReadWriteType.Name} type");
        
        High.Write(Convert.ToByte((Convert.ToUInt16(value.Payload.Data) >> I8BitRegister.Size) & byte.MaxValue));
        Low.Write(Convert.ToByte(Convert.ToUInt16(value.Payload.Data) & byte.MaxValue));
    }

    public override ReadResult Read()
    {
        return new ReadResult((High.Read().Result.Data << I8BitRegister.Size) + Low.Read().Result.Data);
    }
}