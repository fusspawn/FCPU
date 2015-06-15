using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU
{
    public class BasicParser
    {
        public static void LoadCode(FCPU CPU, string Code, bool ResetIP=true) {
            int LineNumber = 0;
            string[] lines = Code.Split(Environment.NewLine.ToCharArray());
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                LineNumber += 1;
                line.Trim();
                try {
                    string[] Parts = line.Split(' ');
                    string[] Args = new ArraySegment<string>(Parts, 1, Parts.Length - 1).ToArray();
                    for (int i = 0;  i < Args.Length; i++) {
                        Args[i] = Args[i].Trim();
                    }
                    CPU.GetInstructionByName(Parts[0].Trim()).Parse(CPU.State, Args);
               } catch (Exception E) {
                    throw new Exception("ParseException: Line: " + LineNumber + " Error: " + E.Message + " Line: " + line);
               }
            }
            if(ResetIP)
                CPU.State.IP = 0;
        }
    }
}
