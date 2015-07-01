

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

        

        public FObject(bool Reg, int Val, FCPUState CPU, int MemoryLoc, bool isSymbol=false) {
            IsRegister = Reg;
            _Value = Val;
            CPUState = CPU;
            Ptr = MemoryLoc;
            IsSymbol = isSymbol;
        }

        public override string ToString()
        {
            return $"Loc: {Ptr} Val: {_Value} Reg: {IsRegister} IsSym: {IsSymbol}";
        }
    }
}
