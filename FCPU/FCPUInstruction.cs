
namespace FCPU
{
    public class FCPUInstruction {
        public int ArgCount = 0;
        public int OpCode = 0;
        public string InsName;
        public string DocString;
        public int[] Args;

        public virtual void Parse(FCPUState State, string[] Args) {
        }
        public virtual void Execute(FCPUState State) { }

        public virtual void Preproc(FCPUState State, string[] Args) {
            State.IP += 1 + ArgCount;
        }

        public virtual void Postproc(FCPUState State, string[] Args) {
            State.IP += 1 + ArgCount;
        }

        public void LoadArgs(FCPUState State)
        {
            Args = new int[ArgCount];
            for (int i = 0; i < ArgCount; i++) {
                Args[i] = State.Memory[(State.IP + 1) + i].Value;
            }
        }
    }
}
