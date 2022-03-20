using Intel8080Emulator.Core.Enums;

namespace Intel8080Emulator.Core.Commands.Exchange;

public class ExchangeCommand : ICommand
{
    public RegisterName First { get; }
    public RegisterName Second { get; }

    public ExchangeCommand(RegisterName first, RegisterName second)
    {
        First = first;
        Second = second;
    }
}