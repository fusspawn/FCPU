﻿using System;

namespace FCPU.Instructions
{
    public class PRINT 
        : FCPUInstruction
    {
        public PRINT() {
            OpCode = 5;
            ArgCount = 0;
            InsName = "PRINT";
            DocString = "PRINT: Print the character in @r0 to the console";
        }

        public override void Execute(FCPUState State)
        {
            ConsoleColor Old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)State.GetRegisterValue(0));
            Console.ForegroundColor = Old;
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
            base.Parse(State, Args);
        }
    }
}
