using Intel8080Emulator.Core.Enums;

namespace Intel8080Emulator.Core.Registers;

public abstract class RegisterBase<TRegisterSize> : IRegister where TRegisterSize : IRegisterSize
{
    public RegisterName Name { get; }
    public int Size => typeof(TRegisterSize).Name switch
    {
        nameof(I8BitRegister) => I8BitRegister.Size,
        nameof(I16BitRegister) => I16BitRegister.Size,
        _ => I8BitRegister.Size,
    };

    protected RegisterBase(RegisterName name)
    {
        Name = name;
    }

    public abstract void Write(WritePayload value);

    public abstract ReadResult Read();

    public override string ToString() => $"{Name}:\t{Convert.ToString(Read().Result.Data, 2).PadLeft(Size, '0')}";
}