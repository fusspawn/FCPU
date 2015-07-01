

namespace FCPU.Instructions
{
    public class NOP 
        : FCPUInstruction
    {
        public NOP() {
            OpCode = 0;
            ArgCount = 0;
            InsName = "NOP";
            DocString = "NOP: No Operation. Waste a cycle doing nothing";
        }

        public override void Execute(FCPUState State)
        {
        }
    }
}
