using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class MOV
        : FCPUInstruction
    {
        public MOV()
        {
            this.OpCode = 0x01;
            this.ArgCount = 2;
            this.InsName = "MOV";
            this.DocString = "MOV val register: move value to register";
        }

        public override void Execute(FCPUState State)
        {
            FObject Source = State.Memory[State.IP + 1];
            FObject Dest = State.Memory[State.IP + 2];

           // Console.WriteLine($"Mov: Source: {((Source.IsRegister == true) ? Source.RegisterName : Source.Value.ToString())}  Dest: {((Dest.IsRegister == true) ? Dest.RegisterName : Dest.Value.ToString())}");
            State.SetRegisterValue(Util.RegisterIndexFromName(Dest.RegisterName), Source.Value);
            base.Execute(State);
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            if (Args.Length != ArgCount)
            {
                throw new Exception("ParseException Invalid OpCode count for: CPY Instruction");
            }

            FObject Source;
            if (Util.IsRegister(Args[0]))
                Source = new FObject(true, Util.RegisterIndexFromName(Args[0]), State, State.IP + 1);
            else if(Util.IsSymbol(Args[0]))
                Source = 
            else
                Source = new FObject(false, int.Parse(Args[0]), State, State.IP + 1);

            if (!Util.IsRegister(Args[1]))
                throw new Exception("Invalid Parse");

            FObject Dest = new FObject(true, Util.RegisterIndexFromName(Args[1]), State, State.IP + 2);
            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.Memory[State.IP + 1] = Source;
            State.Memory[State.IP + 2] = Dest;
            State.IP += this.ArgCount + 1;

            base.Parse(State, Args);
        }
    }
}
