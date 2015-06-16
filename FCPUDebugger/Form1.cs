using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCPU;


namespace FCPUDebugger
{
    public partial class Form1 : Form
    {
        public static int FRAME_BUFFER_WIDTH = 128;
        public static int FRAME_BUFFER_HEIGHT = 128;

        public static int FRAME_BUFFER_LENGTH {
            get { return (FRAME_BUFFER_WIDTH * FRAME_BUFFER_HEIGHT) * 3; }
        }

        FCPU.FCPU CPU = new FCPU.FCPU(new FCPUState(2048 + FRAME_BUFFER_LENGTH));

        public Form1()
        {
            InitializeComponent();

            FCPU.FCPU.InterruptHandlers[0] = (State) =>
            {
                State.IP = 0; // ResetCPU
            };

            FCPU.FCPU.InterruptHandlers[1] = (State) =>
            {
                Console.WriteLine("Interrupt v1");
            };

            FCPU.FCPU.InterruptHandlers[2] = (State) =>
            {
                UpdateFrameBuffer();
            };
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            CPU.State = new FCPUState(2048 + FRAME_BUFFER_LENGTH);
            BasicParser.LoadCodeV2(CPU, SourceCode.Text);
            UpdateDisplays();
            ConsoleListBox.Items.Clear();
        }

        private void UpdateDisplays()
        {
            RegisterListBox.Items.Clear();
            if(UpdateMem.Checked)
                MemoryDisplay.Items.Clear();
            JumpTableDisplay.Items.Clear();
            SymbolTableDisplay.Items.Clear();

            RegisterListBox.Items.Add($"IP: {CPU.State.IP}");

            try
            {
                RegisterListBox.Items.Add($"NextInstruction: {FCPU.FCPU.OpCodes[CPU.State.GetNextOpcode()].InsName}");
            }
            catch (Exception E) {
                RegisterListBox.Items.Add($"NextInstruction: Unknown.. Value: {CPU.State.GetNextOpcode()}");
                var s = E.Message;
            }

            for (int i = 0; i < 5; i++) {
                RegisterListBox.Items.Add($"@r{i}: {CPU.State.GetRegisterValue(i)}");
            }


            if (UpdateMem.Checked)
            {
                foreach (FObject MemoryLoc in CPU.State.Memory)
                {
                    MemoryDisplay.Items.Add(MemoryLoc);
                }
            }

            foreach (var kvp in CPU.State.JumpTable) {
                JumpTableDisplay.Items.Add($"Label: {kvp.Key}, Loc: {kvp.Value}");
            }

            foreach (var kvp in CPU.State.SymbolTable) {
                SymbolTableDisplay.Items.Add($"Label: {kvp.Key}, Ptr: {kvp.Value} Val: {CPU.State.Memory[kvp.Value].Value}");
            }


            if (UpdateMem.Checked)
                MemoryDisplay.SelectedIndex = CPU.State.IP;
        }

        private void ResetCPUButton_Click(object sender, EventArgs e)
        {
            CPU.State = new FCPUState(2048 + FRAME_BUFFER_LENGTH);
            UpdateDisplays();
            ConsoleListBox.Items.Clear();
        }

        private void StepCPUButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < int.Parse(textBox1.Text); i++)
                {
                    CPU.ExecuteStep();
                    Application.DoEvents();
                }
            }
            catch (Exception E) {
                Console.WriteLine(E.Message);
            }
            UpdateDisplays();
            UpdateFrameBuffer();
        }

        private void UpdateFrameBuffer()
        {
           
            pictureBox1.Size = new Size(FRAME_BUFFER_WIDTH, FRAME_BUFFER_HEIGHT);
            Bitmap buffer = new Bitmap(FRAME_BUFFER_WIDTH, FRAME_BUFFER_HEIGHT);
            Graphics Graphics = Graphics.FromImage(buffer);
            pictureBox1.Image = buffer;

            int BUFFER_START = 2048;
            for (int y = 0; y < FRAME_BUFFER_HEIGHT; y++)
            {
                for (int x = 0; x < FRAME_BUFFER_WIDTH; x++)
                {
                    int R = CPU.State.Memory[BUFFER_START].Value;
                    BUFFER_START += 1;
                    int G = CPU.State.Memory[BUFFER_START].Value;
                    BUFFER_START += 1;
                    int B = CPU.State.Memory[BUFFER_START].Value;
                    BUFFER_START += 1;
                    buffer.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
        }

        int get_frame_buffer_index(int x, int y, int width, int offset) { return (x * offset) * (width  * offset) + (y * offset); }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.SetOut(new ListBoxWriter(ConsoleListBox));
            ConsoleListBox.Items.Clear();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
