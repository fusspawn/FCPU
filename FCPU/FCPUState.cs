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
        public int[] Memory = new int[512];

        public Stack<int> Stack = new Stack<int>();

        public int GetNextOpcode() {
            IP += 1;
            return Memory[IP];
        }

        public int GetRegister(int Index) {
            return Registers[Index];
        }

        public int GetRegister(string Name) {
            switch (Name) {
                case "r0": return GetRegister(0);
                case "r1": return GetRegister(1);
                case "r2": return GetRegister(2);
                case "r3": return GetRegister(3);
                case "r4": return GetRegister(4);
                default:
                    throw new Exception("Invalid Register Exception");
                              
            }
        }

        public void PushStack(int Value) {
            Stack.Push(Value);
        }

        public int PopStack(int Value) {
            return Stack.Pop();
        }
    }
}
