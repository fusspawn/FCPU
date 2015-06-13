using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class WRITE_LOC
        : FCPUInstruction
    {
        public WRITE_LOC()
        {
            OpCode = 0x07;
            ArgCount = 0;
            InsName = "WRITE_LOC";
            DocString = "WRITE_LOC: write value stored in @r0 to memory loc stored in @r1";
        }

        public override void Execute(FCPUState State)
        {
            int Ptr = State.GetRegisterValue(1);
            int ValAtPtr = State.GetRegisterValue(0);
            State.Memory[Ptr] = new FObject(false, ValAtPtr, State, Ptr);
            State.IP += 1;
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
