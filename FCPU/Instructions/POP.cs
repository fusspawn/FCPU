using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class POP 
        : FCPUInstruction
    {
        public POP() {
            this.OpCode = 0x01;
            this.ArgCount = 1;
        }

        public override void Execute(FCPUState State)
        {
            base.Execute(State);
        }
    }
}
