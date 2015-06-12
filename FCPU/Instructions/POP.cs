using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class POP 
        : FCPUInstruction
    {
        public POP() {
            this.OpCode = 0x01;
            this.ArgCount = 0;
            this.InsName = "POP";
            this.DocString = "POP: Pops the top value of the stack and puts it in r0";
        }

        public override void Execute(FCPUState State)
        {
            int Value = State.Stack.Pop();
            State.SetRegisterValue(0, Value);
            base.Execute(State);
        }
    }
}
