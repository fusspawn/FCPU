﻿using System;
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
        public FObject[] Memory = new FObject[512];
        public Stack<int> Stack = new Stack<int>();

        public FCPUState() {
            for (int i = 0; i < Memory.Length; i++)
                Memory[i] = new FObject(false, 0x00, this);
        }

        public int GetNextOpcode() {
            return Memory[IP].Value;
        }

        public int GetRegisterValue(int Index) {
            return Registers[Index];
        }

        internal void SetRegisterValue(int index, int value)
        {
            Registers[index] = value;
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
    }
}