using System;

namespace FCPU.Instructions
{
    public class DEBUG
        : FCPUInstruction
    {
        public DEBUG()
        {
            OpCode = 10;
            ArgCount = 1;
            InsName = "DEBUG";
            DocString = "DEBUG: output some usefull state to console";
        }

        public override void Execute(FCPUState State)
        {
            int Val = 0;
            FObject Source = State.Memory[State.IP + 1];
            if (Source.IsRegister)
                Val = State.GetRegisterValue(Util.RegisterIndexFromName(Source.RegisterName));
            else
            {
                Val = State.Memory[Source.Value]._Value;
            }
            Console.WriteLine("DEBUG: val was: " + Val);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.IP += 1;

            FObject Source;
            if (Util.IsRegister(Args[0]))
                Source = new FObject(true, Util.RegisterIndexFromName(Args[0]), State, State.IP);
            else if (Util.IsSymbol(Args[0]))
                Source = new FObject(false, State.SymbolTable[Args[0]], State, State.IP, true);
            else
                Source = new FObject(false, int.Parse(Args[0]), State, State.IP + 1);

            State.Memory[State.IP] = Source;
            State.IP += 1;
        }
    }
}
