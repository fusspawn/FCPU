

namespace FCPU.Instructions
{
    public class LABEL
        : FCPUInstruction
    {
        public LABEL()
        {
            OpCode = 13;
            ArgCount = 1;
            InsName = "LABEL";
            DocString = "LABEL: jump table refrence opcode";
        }

        public override void Execute(FCPUState State)
        {
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.JumpTable[Args[0]] = State.IP;
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, new NOP().OpCode, State, State.IP);
            State.IP += 1;
        }
    }
}
