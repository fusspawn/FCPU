

namespace FCPU.Instructions
{
    public class RET
        : FCPUInstruction
    {
        public RET()
        {
            OpCode = 15;
            ArgCount = 0;
            InsName = "RET";
            DocString = "RET: Return to last Jump point";
        }

        public override void Execute(FCPUState State)
        {
            int NextIP = State.PopStack();
            State.IP = NextIP;
        }            

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
