
namespace FCPU.Instructions
{
    public class LOAD_STR
        : FCPUInstruction
    {
        public LOAD_STR()
        {
            OpCode = 6;
            ArgCount = 0;
            InsName = "LOAD_STR";
            DocString = "LOAD_STR: Load string constant into memory. puts str_len in @r0 and str_start_location in @r1";
        }

        public override void Execute(FCPUState State)
        {
            State.SetRegisterValue(0, State.Memory[State.IP + 1].Value);
            State.SetRegisterValue(1, State.IP + 2);
            State.IP += State.Memory[State.IP + 1].Value + 2;
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, Args[0].Length, State, State.IP);
            State.IP += 1;
            State.Memory[State.IP] = new FObject(false, State.IP + 1, State, State.IP);
            State.IP += 1;

            foreach (char c in Args[0])
            {
                State.Memory[State.IP] = new FObject(false, (int)c, State, State.IP);
                State.IP += 1;
            }
        }
    }
}
