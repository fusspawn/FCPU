using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class JMP
        : FCPUInstruction
    {
        public JMP()
        {
            OpCode = 14;
            ArgCount = 1;
            InsName = "JMP";
            DocString = "JMP: Jump to Label";
        }

        public override void Execute(FCPUState State)
        {
            State.PushStack(State.IP + 2);
            State.IP = State.Memory[State.IP + 1].Value;
        }            

        public override void Parse(FCPUState State, string[] Args)
        {
            if (!State.JumpTable.ContainsKey(Args[0]))
            {
                throw new Exception("Jmp, Label not exists: " + Args[0]);
            }
            else {
                State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
                State.IP += 1;
                State.Memory[State.IP] = new FObject(false, State.JumpTable[Args[0]], State, State.IP);
                State.IP += 1;
            }
        }
    }
}
