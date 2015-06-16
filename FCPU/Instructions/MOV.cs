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
            this.OpCode = 1;
            this.ArgCount = 2;
            this.InsName = "MOV";
            this.DocString = "MOV val register: move value to register";
        }

        public override void Execute(FCPUState State)
        {
            FObject Source = State.Memory[State.IP + 1];
            FObject Dest = State.Memory[State.IP + 2];

            //Console.WriteLine($"Mov: Source: IsReg: {Source.IsRegister} IsSymbol: {Source.IsSymbol} Val: {Source.Value}, Dest IsReg: {Dest.IsRegister} IsSymbol: {Dest.IsSymbol} Val: {Dest.Value} ");
            if (Dest.IsRegister)
                State.SetRegisterValue(Util.RegisterIndexFromName(Dest.RegisterName), Source.IsSymbol == true ? State.Memory[Source.Value].Value : Source.Value);
            else if (Dest.IsSymbol)
            {
                var tobject = State.Memory[Dest.Value];
                tobject.Value = Source.Value;
                State.Memory[Dest.Value] = tobject;
            }
            else {
                //DMA
                var tobject = State.Memory[Dest.Value];
                tobject.Value = Source.Value;
                State.Memory[Dest.Value] = tobject;
            }
        }

        public override void Parse(FCPUState State, string[] Args)
        {
            
            if (Args.Length != ArgCount)
            {
                throw new Exception("ParseException Invalid OpCode count for: MOV Instruction");
            }

            FObject Source;
            FObject Dest;

            if (Util.IsRegister(Args[0]))
                Source = new FObject(true, Util.RegisterIndexFromName(Args[0]), State, State.IP + 1);
            else if (Util.IsSymbol(Args[0]))
                Source = new FObject(false, State.SymbolTable[Args[0]], State, State.IP + 1, true);
            else
                Source = new FObject(false, int.Parse(Args[0]), State, State.IP + 1);

            if (Util.IsRegister(Args[1]))
                Dest = new FObject(true, Util.RegisterIndexFromName(Args[1]), State, State.IP + 2);
            else if (Util.IsSymbol(Args[1]))
                Dest = new FObject(false, State.SymbolTable[Args[1]], State, State.IP + 2, true);
            else
                Dest = new FObject(false, int.Parse(Args[1]), State, State.IP + 2, false);

            State.Memory[State.IP] = new FObject(false, this.OpCode, State, State.IP);
            State.Memory[State.IP + 1] = Source;
            State.Memory[State.IP + 2] = Dest;
            State.IP += this.ArgCount + 1;
            base.Parse(State, Args);
        }
    }
}
