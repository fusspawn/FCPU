using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class PUSH
        : FCPUInstruction
    {
        public PUSH()
        {
            this.OpCode = 0x02;
            this.ArgCount = 0;
            this.InsName = "PUSH";
            this.DocString = "PUSH: Push's value in r0 to stack";
        }

        public override void Execute(FCPUState State)
        {
            int Value = State.GetRegisterValue(0);
            State.Stack.Push(Value);
            base.Execute(State);
        }
    }
}
