using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class CAST_INT
        : FCPUInstruction
    {
        public CAST_INT()
        {
            OpCode = 12;
            ArgCount = 0;
            InsName = "CAST_INT";
            DocString = "CAST_INT: take the value in @r0 convert it to its int value, and store that in @r1";
        }

        public override void Execute(FCPUState State)
        {
            int r0 = (char)State.GetRegisterValue(0);
            if (r0 < 0 || r0 > 9)
                throw new Exception($"CAST_CHAR: Can only cast single digit characters [0-9] not :" + r0);
            else
                State.SetRegisterValue(1, r0);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
