using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU
{
    public class FCPUState
    {
        public int IP = 0;
        public int[] Registers = new int[5];
        public FObject[] Memory = new FObject[1024];
        public Stack<int> Stack = new Stack<int>();
        public Dictionary<string, int> JumpTable;
        public Dictionary<string, int> SymbolTable;

        public FCPUState() {
            for (int i = 0; i < Memory.Length; i++)
                Memory[i] = new FObject(false, 0x00, this, i);
            JumpTable = new Dictionary<string, int>();
        }

        public int GetNextOpcode() {
            return Memory[IP].Value;
        }

        public int GetRegisterValue(int Index) {
            //Console.WriteLine("Read Value: " + Registers[Index] + " from register: @r" + Index);
            return Registers[Index];
        }

        public void SetRegisterValue(int index, int value)
        {
            Registers[index] = value;
            //Console.WriteLine("Wrote Value: " + value + " to register: @r" + index);
        }

        public int GetRegisterValue(string Name) {
            switch (Name) {
                case "r0": return GetRegisterValue(0);
                case "r1": return GetRegisterValue(1);
                case "r2": return GetRegisterValue(2);
                case "r3": return GetRegisterValue(3);
                case "r4": return GetRegisterValue(4);
                default:
                    throw new Exception("Invalid Register Exception");                              
            }
        }

        public void PushStack(int Value) {
            Stack.Push(Value);
        }

        public int PopStack() {
            return Stack.Pop();
        }

        public int GetSymbolValue(string Symbol) {
            return Memory[SymbolTable[Symbol]].Value;
        }

        public void SetSymbolValue(string Symbol, int Value) {
            Memory[SymbolTable[Symbol]].Value = Value;
        }
    }
}
