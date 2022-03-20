namespace Intel8080Emulator.Core.Registers;

public interface IRegisterSize
{
    static int Size { get; }
    static Type ReadWriteType { get; }
}

public interface I8BitRegister : IRegisterSize
{
    new static int Size => 8;
    new static Type ReadWriteType => typeof(byte);
}

public interface I16BitRegister : IRegisterSize
{
    new static int Size => 16;
    new static Type ReadWriteType => typeof(ushort);
}

