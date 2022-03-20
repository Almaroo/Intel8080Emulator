using Intel8080Emulator.Core.Enums;

namespace Intel8080Emulator.Core.Commands.Move;

public class MoveCommand : ICommand
{
    public RegisterName Target { get; }
    public RegisterName? Source { get; }
    public ushort? Value { get; }

    public MoveCommand(RegisterName target, ushort value)
    {
        Target = target;
        Value = value;
    }

    public MoveCommand(RegisterName target, RegisterName source)
    {
        Target = target;
        Source = source;
    }
}
