using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU
{
    public class FCPUInstruction {
        public int ArgCount = 0;
        public int OpCode = 0;
        public int[] Args;

        public virtual void Execute(FCPUState State) {

        }

        public void LoadArgs(FCPUState State)
        {
            Args = new int[ArgCount - 1];
            for (int i = 0; i < ArgCount; i++) {
                Args[i] = State.Memory[(State.IP + 1) + i];
            }
        }
    }
}
