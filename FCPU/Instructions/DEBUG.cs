using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class DEBUG
        : FCPUInstruction
    {
        public DEBUG()
        {
            OpCode = 0x10;
            ArgCount = 0;
            InsName = "DEBUG";
            DocString = "DEBUG: output some usefull state to console";
        }

        public override void Execute(FCPUState State)
        {
            FCPU.DumpStateToConsole(State);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
