using Intel8080Emulator.Core.Commands;
using Intel8080Emulator.Core.Commands.Move;
using Intel8080Emulator.Core.Registers;

namespace Intel8080Emulator.Core;

public partial class Intel8080 : IHandle<MoveCommand>
{
    #region Handles<MoveCommand> implementation

    public void Handle(MoveCommand command)
    {
        OnLogCreated(new LogAppendedEventArgs
        {
            Timestamp = DateTime.Now, 
            Log = $"Command {nameof(MoveCommand)} received",
        });
        
        try
        {
            if (!ValidateRegisterExists(command.Target, out var target))
                throw new ArgumentException($"No such register: {command.Target}");

            if (command.Source.HasValue && Registers.TryGetValue(command.Source.Value, out var source))
            {
                if (target.Size < source.Size)
                    throw new ArgumentException($"Target register cannot be smaller than source register");
                
                target.Write(source.Read());
            }

            else if (command.Value.HasValue)
            {
                target.Write(new WritePayload(command.Value.Value));
            }
        
            OnLogCreated(new LogAppendedEventArgs
            {
                Timestamp = DateTime.Now,
                Log = $"Command {nameof(MoveCommand)} handled successfully\n{ToString()}",
            });
        }
        catch (Exception e)
        {
            OnLogCreated(new LogAppendedEventArgs
            {
                Timestamp = DateTime.Now,
                Log = $"Command {nameof(MoveCommand)} failed:\t{e.Message}\n{ToString()}",
            });
        }
    }

    #endregion
}