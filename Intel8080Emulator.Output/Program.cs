 using Intel8080Emulator.Core;
 using Intel8080Emulator.Core.Commands.Exchange;
 using Intel8080Emulator.Core.Commands.Move;
 using Intel8080Emulator.Core.Enums;

 var i8080 = Intel8080
     .Create()
     .AddRegister(RegisterName.AL)
     .AddRegister(RegisterName.AH)
     .CombineRegisters(RegisterName.AX, RegisterName.AL, RegisterName.AH)
     .AddRegister(RegisterName.BL)
     .AddRegister(RegisterName.BH)
     .CombineRegisters(RegisterName.BX, RegisterName.BL, RegisterName.BH)
     .AddRegister(RegisterName.CL)
     .AddRegister(RegisterName.CH)
     .CombineRegisters(RegisterName.CX, RegisterName.CL, RegisterName.CH)
     .AddRegister(RegisterName.DL)
     .AddRegister(RegisterName.DH)
     .CombineRegisters(RegisterName.DX, RegisterName.DL, RegisterName.DH)
     .Build();

i8080.LogAppended += (_, args) =>
{
     Console.ForegroundColor = ConsoleColor.Yellow;
     Console.WriteLine(args.Timestamp);
     Console.ResetColor();
     Console.WriteLine(args.Log); 
};
 
i8080.Handle(new MoveCommand(RegisterName.AL, 255));

i8080.Handle(new MoveCommand(RegisterName.AH, RegisterName.AL));

i8080.Handle(new MoveCommand(RegisterName.AX, (ushort) 0));

i8080.Handle(new MoveCommand(RegisterName.AX, 511));

i8080.Handle(new ExchangeCommand(RegisterName.AL, RegisterName.AH));

i8080.Handle(new MoveCommand(RegisterName.BX, RegisterName.AX));
 
i8080.Handle(new MoveCommand(RegisterName.AL, RegisterName.AX));

i8080.Handle(new ExchangeCommand(RegisterName.AX, RegisterName.AL));