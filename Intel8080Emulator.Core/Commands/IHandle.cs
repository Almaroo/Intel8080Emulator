namespace Intel8080Emulator.Core.Commands;

public interface IHandle<TCommand> where TCommand: ICommand
{
    public void Handle(TCommand command);
}