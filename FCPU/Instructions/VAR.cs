﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class VAR
        : FCPUInstruction
    {
        public VAR() {
            OpCode = 0x16;
            ArgCount = 1;
            InsName = "VAR";
            DocString = "VAR: Alloc variable store in memory";
        }

        public override void Execute(FCPUState State)
        {
        }            

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, 0, State, State.IP);
            State.SymbolTable.Add(Args[0], State.IP);
            State.IP += 1;
        }
    }
}
