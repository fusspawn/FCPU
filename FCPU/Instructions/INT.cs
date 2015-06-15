using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class INT
        : FCPUInstruction
    {
        public INT() {
            OpCode = 17;
            ArgCount = 1;
            InsName = "INT";
            DocString = "INT: call interrupt handler - handler id in @r0";
        }

        public override void Execute(FCPUState State)
        {
            FObject Arg = State.Memory[State.IP + 1];
            if (!FCPU.InterruptHandlers.ContainsKey(Arg.Value))
            {
                throw new Exception("Invalid interupt call, No handler for ID: " + Arg.Value);
            }
            else {
                FCPU.InterruptHandlers[Arg.Value](State);
            }
        }            

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, int.Parse(Args[0]), State, State.IP, true);
            State.IP += 1;
        }
    }
}
