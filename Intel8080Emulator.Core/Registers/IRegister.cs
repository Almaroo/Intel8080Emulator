namespace Intel8080Emulator.Core.Registers;

public interface IRegister
{
    public int Size { get; }
    public void Write(WritePayload value);
    public ReadResult Read();
}
