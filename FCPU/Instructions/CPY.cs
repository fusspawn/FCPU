﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class CPY
        : FCPUInstruction
    {
        public CPY()
        {
            this.OpCode = 0x03;
            this.ArgCount = 2;
            this.InsName = "CPY";
            this.DocString = "CPY <sourceregister or value> <dest register>: move value to register";
        }

        public override void Execute(FCPUState State)
        {
            FObject Source = State.Memory[State.IP + 1];
            FObject Dest = State.Memory[State.IP + 2];
            Dest.Value = Source.Value;
            base.Execute(State);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            if (Args.Length != ArgCount) {
                throw new Exception("ParseException Invalid OpCode count for: CPY Instruction");
            }

            FObject Source;
            if (Util.IsRegister(Args[0]))
                Source = new FObject(true, Util.RegisterIndexFromName(Args[0]), State);
            else
                Source = new FObject(false, int.Parse(Args[0]), State);

            if (!Util.IsRegister(Args[1]))
                throw new Exception("Invalid Parse");

            FObject Dest = new FObject(true, Util.RegisterIndexFromName(Args[1]), State);
            State.Memory[State.IP] = new FObject(false, this.OpCode, State);
            State.Memory[State.IP + 1] = Source;
            State.Memory[State.IP + 2] = Dest;
            State.IP += this.ArgCount + 1;
            base.Parse(State, Args);
        }
    }
}