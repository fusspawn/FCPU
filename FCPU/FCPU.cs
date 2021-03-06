﻿using FCPU.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FCPU
{
    public class FCPU
    {
        public FCPUState State;
        public static Dictionary<int, FCPUInstruction> OpCodes;
        public static Dictionary<int, Action<FCPUState>> InterruptHandlers;

        public FCPU(FCPUState State) {
            this.State = State;
            OpCodes = new Dictionary<int, FCPUInstruction>();
            InterruptHandlers = new Dictionary<int, Action<FCPUState>>();
            RegisterInstruction(new NOP());
            RegisterInstruction(new MOV());
            RegisterInstruction(new ADD());
            RegisterInstruction(new DIV());
            RegisterInstruction(new MUL());
            RegisterInstruction(new PRINT());
            RegisterInstruction(new LOAD_STR());
            RegisterInstruction(new PRINT_STR());
            RegisterInstruction(new READ_LOC());
            RegisterInstruction(new WRITE_LOC());
            RegisterInstruction(new DEBUG());
            RegisterInstruction(new CAST_CHAR());
            RegisterInstruction(new LABEL());
            RegisterInstruction(new JMP());
            RegisterInstruction(new RET());
            RegisterInstruction(new VAR());
            RegisterInstruction(new INT());
            RegisterInstruction(new JNZ());
            RegisterInstruction(new JGZ());
            RegisterInstruction(new JLZ());
            RegisterInstruction(new JEZ());
            RegisterInstruction(new RAND());
            RegisterInstruction(new SUB());
        }

        public void RegisterInstruction(FCPUInstruction Instruction) {
            if (OpCodes.ContainsKey(Instruction.OpCode))  {
                throw new Exception("Duplicate OpCode Id");
            } else {
                OpCodes[Instruction.OpCode] = Instruction;
            }
        }

        public void ExecuteStep() {
            try
            {
                int Instruction = State.GetNextOpcode();
                if (!OpCodes.ContainsKey(Instruction))
                {
                    throw new Exception("Invalid Opcode");
                }
                else
                {
                    OpCodes[Instruction].LoadArgs(State);
                    OpCodes[Instruction].Execute(State);
                    State.IP += OpCodes[Instruction].ArgCount + 1;
                }
            }
            catch (Exception E) {
                Console.WriteLine("Runtime Exception: " + E.Message);
            }
        }

        public FCPUInstruction GetInstructionByName(string Name) {
            return (from p in OpCodes where p.Value.InsName == Name select p.Value).FirstOrDefault();
        }

        public static void DumpStateToConsole(FCPUState State) {
            Console.WriteLine($"IP: {State.IP}, @r0: {State.GetRegisterValue(0)}  @r1: {State.GetRegisterValue(1)} @r2: {State.GetRegisterValue(2)} @r3: {State.GetRegisterValue(3)} @r4: {State.GetRegisterValue(4)}");
            Console.WriteLine($"Current Instruction at IP: {OpCodes[State.GetNextOpcode()].InsName}");
        } 
    }
}
