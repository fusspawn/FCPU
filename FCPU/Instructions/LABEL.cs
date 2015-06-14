﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class LABEL
        : FCPUInstruction
    {
        public LABEL()
        {
            OpCode = 0x13;
            ArgCount = 1;
            InsName = "LABEL";
            DocString = "LABEL: jump table refrence opcode";
        }

        public override void Execute(FCPUState State)
        {
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.JumpTable.Add(Args[0], State.IP);
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, 0x00, State, State.IP);
            State.IP += 1;
        }
    }
}