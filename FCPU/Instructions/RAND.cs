using System;

namespace FCPU.Instructions
{
    public class RAND
        : FCPUInstruction
    {
        static Random Rand = new Random();
        public RAND() {
            OpCode = 22;
            ArgCount = 2;
            InsName = "RAND";
            DocString = "RAND: puts a random int between arg0 and arg1 into @r0";
        }

        public override void Execute(FCPUState State)
        {
            int From = State.Memory[State.IP + 1].Value;
            int To = State.Memory[State.IP + 2].Value;
            int Rand = RAND.Rand.Next(From, To);
            State.SetRegisterValue(0, Rand);
        }            

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;         
            State.Memory[State.IP] = new FObject(false, int.Parse(Args[0]), State, State.IP, true);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, int.Parse(Args[1]), State, State.IP, true);
            State.IP += 1;
        }
    }
}
