using System.Collections.Specialized;
using Intel8080Emulator.Core.Enums;

namespace Intel8080Emulator.Core.Registers;

internal class Register : RegisterBase<I8BitRegister>
{
    private BitVector32 _bits;

    internal Register(RegisterName name) : base(name) { }

    public bool this[int idx]
    {
        get
        {
            if (idx >= 0 && idx < Size)
            {
                return _bits[idx];
            }

            throw new ArgumentOutOfRangeException(
                $"Register can store only {Size}bits, consider using {2 * Size}bits register combination");
        }

        set
        {
            if (idx >= 0 && idx < Size)
            {
                _bits[idx] = value;
            }

            throw new ArgumentOutOfRangeException(
                $"Register can store only {Size}bits, consider using {2 * Size}bits register combination");
        }
    }

    
    public void Write(byte value) => _bits = new BitVector32(value);

    public override void Write(WritePayload value)
    {
        if (value.Payload.Data > byte.MaxValue)
            throw new ArgumentException(
                $"Payload exceeded {I8BitRegister.Size}bit size, make sure to use {I8BitRegister.ReadWriteType.Name} type");
        _bits = value.Payload;
    }

    public override ReadResult Read()
    {
        return new ReadResult(_bits);
    }
}