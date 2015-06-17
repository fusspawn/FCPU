using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class JEZ
        : FCPUInstruction
    {
        public JEZ() {
            OpCode = 19;
            ArgCount = 1;
            InsName = "JEZ";
            DocString = "JNZ: call interrupt handler - handler id in @r0";
        }

        public override void Execute(FCPUState State)
        {
            FObject Arg = State.Memory[State.IP + 1];
            if(State.GetRegisterValue(0) == 0)
               State.IP = Arg.Value - 2; // Minus 2 because the ip will auto increment by 2 after executing this instruction
        }            

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;         

            State.Memory[State.IP] = new FObject(false, State.JumpTable[Args[0]], State, State.IP, true);
            State.IP += 1;
        }
        public override void Postproc(FCPUState State, string[] Args)
        {
            var Current = State.Memory[State.IP + 1];

            if (!(Current.Value > 0))
            {
                Current.Value = State.JumpTable[Args[0]];
                State.Memory[Current.Ptr] = Current;
                Console.WriteLine("Fixed JMP Opcode");
            }
            else
            {
                Console.WriteLine($"JMP_TABLE_OKAY: No need to fix");
            }

            base.Postproc(State, Args);
        }
    }
}
