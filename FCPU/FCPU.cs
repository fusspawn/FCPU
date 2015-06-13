using FCPU.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU
{
    public class FCPU
    {
        public FCPUState State;
        public Dictionary<int, FCPUInstruction> OpCodes;

        public FCPU(FCPUState State) {
            this.State = State;
            this.OpCodes = new Dictionary<int, FCPUInstruction>();
            RegisterInstruction(new NOP());
            RegisterInstruction(new MOV());
            RegisterInstruction(new ADD());
            RegisterInstruction(new DIV());
            RegisterInstruction(new MUL());
            RegisterInstruction(new PRINT());
            RegisterInstruction(new LOAD_STR());
            RegisterInstruction(new PRINT_STR());
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
                State.IP += OpCodes[Instruction].ArgCount + 1;
            }
        }

        public FCPUInstruction GetInstructionByName(string Name) {
            return (from p in OpCodes where p.Value.InsName == Name select p.Value).FirstOrDefault();
        }

        public void DumpStateToConsole() {
            Console.WriteLine($"IP: {State.IP}, @r0: {State.GetRegisterValue(0)}  @r1: {State.GetRegisterValue(1)} @r2: {State.GetRegisterValue(2)} @r3: {State.GetRegisterValue(3)} @r4: {State.GetRegisterValue(4)}");
            Console.WriteLine($"Current Instruction at IP: {OpCodes[State.GetNextOpcode()].InsName}");
        } 
    }
}
