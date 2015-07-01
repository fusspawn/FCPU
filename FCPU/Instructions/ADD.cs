

namespace FCPU.Instructions
{
    public class ADD
        : FCPUInstruction
    {
        public ADD()
        {
            OpCode = 2;
            ArgCount = 0;
            InsName = "ADD";
            DocString = "ADD: add the first two values on the stack. result goes in r0";
        }

        public override void Execute(FCPUState State)
        {
            int A = State.GetRegisterValue(0);
            int B = State.GetRegisterValue(1);
            State.SetRegisterValue(0, A + B);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += this.ArgCount + 1;            
        }
    }
}
