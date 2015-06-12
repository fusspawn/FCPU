using System;
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
        public FCPUState CPUState;

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

        public FObject(bool Reg, int Val, FCPUState CPU) {
            IsRegister = Reg;
            _Value = Val;
            CPUState = CPU;
        }
    }
}
