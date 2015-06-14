﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU
{
    public class FObject
    {
        public int _Value;
        public bool IsRegister;
        public bool IsSymbol;
        public FCPUState CPUState;
        public int Ptr;

        public int Value
        {
            get {
                 if (IsRegister)
                    return CPUState.GetRegisterValue(_Value);
                 else
                    return _Value; 
            }

            set {
                if (IsRegister)
                    CPUState.SetRegisterValue(_Value, value);
                else
                    _Value = value;
            }
        }

        public string RegisterName {
            get {
                return "@r" + _Value;
            }
        }

        

        public FObject(bool Reg, int Val, FCPUState CPU, int MemoryLoc) {
            IsRegister = Reg;
            _Value = Val;
            CPUState = CPU;
            Ptr = MemoryLoc;
        }

        public override string ToString()
        {
            return $"Loc: {Ptr} Val: {_Value} Reg: {IsRegister}";
        }
    }
}
