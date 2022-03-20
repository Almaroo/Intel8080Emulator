using System.Text;
using Intel8080Emulator.Core.Enums;
using Intel8080Emulator.Core.Registers;

namespace Intel8080Emulator.Core;

public sealed partial class Intel8080
{
    public readonly Dictionary<RegisterName, IRegister> Registers = new();

    public event EventHandler<LogAppendedEventArgs> LogAppended; 

    internal Intel8080()
    {
    }

    public class Intel8080Builder
    {
        private Intel8080 _intel = new();

        public Intel8080Builder AddRegister(RegisterName registerName)
        {
            _intel.Registers.Add(registerName, new Register(registerName));
            return this;
        }

        public Intel8080Builder CombineRegisters(RegisterName combinedRegisterName, RegisterName lowRegisterName,
            RegisterName highRegisterName)
        {
            if (!_intel.Registers.ContainsKey(lowRegisterName))
                throw new ArgumentException($"Register {lowRegisterName} was not registered yet");

            if (!_intel.Registers.ContainsKey(highRegisterName))
                throw new ArgumentException($"Register {highRegisterName} was not registered yet");

            _intel.Registers.Add(combinedRegisterName, new CombinedRegister(combinedRegisterName,
                (Register) _intel.Registers[lowRegisterName], (Register) _intel.Registers[highRegisterName]));
            return this;
        }

        public Intel8080 Build()
        {
            return _intel;
        }
    }

    public static Intel8080Builder Create() => new();
    
    private bool ValidateRegisterExists(RegisterName registerName, out IRegister register) => Registers.TryGetValue(registerName, out register!);

    public void OnLogCreated(LogAppendedEventArgs args)
    {
        var handler = LogAppended;
        handler(this, args);
    }
    
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("-------- INTEL8080 EMULATOR --------");
        stringBuilder.AppendLine($"Available registers: [{Registers.Count}]");
        foreach (var register in Registers.Values)
        {
            stringBuilder.AppendLine($"{register.ToString()}");
            stringBuilder.AppendLine();
        }
        stringBuilder.AppendLine("------------------------------------");
        return stringBuilder.ToString();
    }
}

public class LogAppendedEventArgs : EventArgs
{
    public DateTime Timestamp { get; init; }
    public string Log { get; init; }
}