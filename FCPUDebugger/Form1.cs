﻿using System;
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

            RegisterListBox.Items.Add($"IP: {CPU.State.IP}");
            try
            {
                RegisterListBox.Items.Add($"NextInstruction: {FCPU.FCPU.OpCodes[CPU.State.GetNextOpcode()].InsName}");
            }
            catch (Exception E) {
                RegisterListBox.Items.Add($"NextInstruction: Unknown.. Value: {CPU.State.GetNextOpcode()}");
            }
            for (int i = 0; i < 5; i++) {
                RegisterListBox.Items.Add($"@r{i}: {CPU.State.GetRegisterValue(i)}");
            }

            foreach (FObject MemoryLoc in CPU.State.Memory) {
                MemoryDisplay.Items.Add(MemoryLoc);
            }

            MemoryDisplay.SelectedIndex = CPU.State.IP;
        }

        private void ResetCPUButton_Click(object sender, EventArgs e)
        {
            CPU.State.IP = 0;
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
    }
}