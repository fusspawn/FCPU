using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCPU.Instructions
{
    public class NOP 
        : FCPUInstruction
    {
        public NOP() {
            OpCode = 0x00;
            ArgCount = 0;
        }

        public override void Execute(FCPUState State)
        {
        }
    }
}
