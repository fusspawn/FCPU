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
        FCPU.FCPU CPU = new FCPU.FCPU(new FCPUState());

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
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            CPU.State = new FCPUState();
            BasicParser.LoadCode(CPU, SourceCode.Text);
            UpdateDisplays();
            ConsoleListBox.Items.Clear();
        }

        private void UpdateDisplays()
        {
            RegisterListBox.Items.Clear();
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

            foreach (FObject MemoryLoc in CPU.State.Memory) {
                MemoryDisplay.Items.Add(MemoryLoc);
            }

            foreach (var kvp in CPU.State.JumpTable) {
                JumpTableDisplay.Items.Add($"Label: {kvp.Key}, Loc: {kvp.Value}");
            }

            foreach (var kvp in CPU.State.SymbolTable) {
                SymbolTableDisplay.Items.Add($"Label: {kvp.Key}, Ptr: {kvp.Value} Val: {CPU.State.Memory[kvp.Value].Value}");
            }

            MemoryDisplay.SelectedIndex = CPU.State.IP;
        }

        private void ResetCPUButton_Click(object sender, EventArgs e)
        {
            CPU.State = new FCPUState();
            UpdateDisplays();
            ConsoleListBox.Items.Clear();
        }

        private void StepCPUButton_Click(object sender, EventArgs e)
        {
            CPU.ExecuteStep();
            UpdateDisplays();
        }

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
