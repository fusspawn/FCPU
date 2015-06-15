using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class CAST_CHAR
        : FCPUInstruction
    {
        public CAST_CHAR()
        {
            OpCode = 11;
            ArgCount = 0;
            InsName = "CAST_CHAR";
            DocString = "CAST_CHAR: take the value in @r0 convert it to its character display code, and store that in @r1";
        }

        public override void Execute(FCPUState State)
        {
            int r0 = State.GetRegisterValue(0);
            if (r0 < 0 || r0 > 9)
                throw new Exception($"CAST_CHAR: Can only cast single digit characters [0-9] not :" + r0);
            else
                State.SetRegisterValue(1, (int)(r0.ToString()[0]));
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
