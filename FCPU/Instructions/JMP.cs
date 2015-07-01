using System;
using System.Collections.Generic;

namespace FCPU.Instructions
{
    public class JMP
        : FCPUInstruction
    {
        public static Dictionary<int, string> ToFix = new Dictionary<int, string>();

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
                State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
                State.IP += 1;
                State.Memory[State.IP] = new FObject(false, -1, State, State.IP);
                ToFix.Add(State.IP, Args[0]);
                State.IP += 1;
            }
            else {
                State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
                State.IP += 1;
                State.Memory[State.IP] = new FObject(false, State.JumpTable[Args[0]], State, State.IP);
                State.IP += 1;
            }
        }

        public override void Postproc(FCPUState State, string[] Args)
        {
            var Current = State.Memory[State.IP + 1];

            if (!(Current.Value > 0)) {
                Current.Value = State.JumpTable[Args[0]];
                State.Memory[Current.Ptr] = Current;
                Console.WriteLine("Fixed JMP Opcode");
            } else
            {
                Console.WriteLine($"JMP_TABLE_OKAY: No need to fix");
            }

            base.Postproc(State, Args);
        }
    }
}
