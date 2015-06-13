using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class PRINT_STR
        : FCPUInstruction
    {
        public PRINT_STR()
        {
            OpCode = 0x07;
            ArgCount = 0;
            InsName = "PRINT_STR";
            DocString = "PRINT_STR: Prints the results of LOAD_STR to console. LOAD_STR MUST!! be called as the instruction Prior.";
        }

        public override void Execute(FCPUState State)
        {
            int Length = State.GetRegisterValue(0);
            int Start = State.GetRegisterValue(1);
            var S = new StringBuilder();

            for (int i = 0; i < Length; i++) {
               S.Append((char)State.Memory[Start + i].Value);
            }

            Console.WriteLine(S.ToString());
            State.IP = Start + Length + 1;
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
