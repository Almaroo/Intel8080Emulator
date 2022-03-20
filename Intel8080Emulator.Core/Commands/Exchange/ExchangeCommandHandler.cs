using Intel8080Emulator.Core.Commands;
using Intel8080Emulator.Core.Commands.Exchange;

namespace Intel8080Emulator.Core;

public partial class Intel8080 : IHandle<ExchangeCommand>
{
    #region Handles<ExchangeCommand> implementation
    public void Handle(ExchangeCommand command)
    {
        
        OnLogCreated(new LogAppendedEventArgs
        {
            Timestamp = DateTime.Now, 
            Log = $"Command {nameof(ExchangeCommand)} received",
        });

        try
        {
            if (!ValidateRegisterExists(command.First, out var first))
                throw new ArgumentException($"No such register: {command.First}");
        
            if (!ValidateRegisterExists(command.Second, out var second))
                throw new ArgumentException($"No such register: {command.Second}");

            if (first.Size != second.Size)
                throw new ArgumentException($"Registers cannot vary in size");
            
            var tmp = first.Read();
            first.Write(second.Read());
            second.Write(tmp);
        
            OnLogCreated(new LogAppendedEventArgs
            {
                Timestamp = DateTime.Now,
                Log = $"Command {nameof(ExchangeCommand)} handled successfully\n{ToString()}",
            });
        }
        catch (Exception e)
        {
            OnLogCreated(new LogAppendedEventArgs
            {
                Timestamp = DateTime.Now,
                Log = $"Command {nameof(ExchangeCommand)} failed\t{e.Message}\n{ToString()}",
            });
        }
    }
    
    #endregion
}