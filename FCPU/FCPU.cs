using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU
{
    public class FCPU
    {
        FCPUState State;
        Dictionary<int, FCPUInstruction> OpCodes;

        public FCPU(FCPUState State) {
            this.State = State;
            this.OpCodes = new Dictionary<int, FCPUInstruction>();
        }

        public void RegisterInstruction(FCPUInstruction Instruction) {
            if (OpCodes.ContainsKey(Instruction.OpCode))  {
                throw new Exception("Duplicate OpCode Id");
            } else {
                OpCodes[Instruction.OpCode] = Instruction;
            }
        }

        public void ExecuteStep() {
            int Instruction = State.GetNextOpcode();
            if (!OpCodes.ContainsKey(Instruction)) {
                throw new Exception("Invalid Opcode");
            }
            else {
                OpCodes[Instruction].LoadArgs(State);
                OpCodes[Instruction].Execute(State);
            }
        }        
    }
}
