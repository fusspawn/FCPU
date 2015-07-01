using FCPU.Instructions;
using System;

namespace FCPU
{
    class MemoryManager
    {
        public static int GetValueForRefrence(FCPUState State, string refstring)
        {
            if (refstring.StartsWith("@r"))
                return State.GetRegisterValue(refstring);

            if (refstring.StartsWith("$"))
                return State.Memory[State.SymbolTable[refstring]].Value;

            if (refstring.StartsWith("#"))
                return DMA_READ(State, refstring);

            return int.Parse(refstring);
        }

        public static int SetValueForReference(FCPUState State, string refstring, int value)
        {

            if (refstring.StartsWith("@r"))
            {
                State.SetRegisterValue(Util.RegisterIndexFromName(refstring), value);
            }

            if (refstring.StartsWith("$"))
            {
                State.SetSymbolValue(refstring, value);
            }

            if (refstring.StartsWith("#"))
            {
                DMA_WRITE(State, refstring, value);
            }

            throw new Exception("invalid memory refrence: " + refstring);
        }

        private static int DMA_READ(FCPUState State, string refstring)
        {
            string remain = refstring.Remove(0, 1);
            int val = int.Parse(remain);
            return State.Memory[val].Value;
        }

        private static void DMA_WRITE(FCPUState state, string refstring, int value)
        {
            string remain = refstring.Remove(0, 1);
            int val = int.Parse(remain);
            state.Memory[val].Value = value;
        }      
    }
}
