﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class MUL
        : FCPUInstruction
    {
        public MUL()
        {
            OpCode = 0x04;
            ArgCount = 0;
            InsName = "MUL";
            DocString = "MUL: add the first two values on the stack. result goes in r0";
        }

        public override void Execute(FCPUState State)
        {
            int A = State.GetRegisterValue(0);
            int B = State.GetRegisterValue(1);
            State.SetRegisterValue(0, A * B);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += this.ArgCount + 1;            
        }
    }
}