using System;

namespace FCPU.Instructions
{
    public class Util
    {
        public static bool IsRegister(string Val) {
            return Val.StartsWith("@r");
        }

        public static int RegisterIndexFromName(string Val) {
            int outval;
            bool okay = int.TryParse(Val.Substring(2, 1), out outval);
            if (okay)
            {
                return outval;
            }
            else {
                throw new Exception("Invalid Register Name: " + Val);
            }
        }

        internal static bool IsSymbol(string v)
        {
            throw new NotImplementedException();
        }
    }
}