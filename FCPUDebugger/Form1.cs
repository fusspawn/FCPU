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
using System.IO;
using System.Xml;
using System.Windows.Forms.Integration;
using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Windows.Input;
using System.Windows.Documents;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;

namespace FCPUDebugger
{
    public partial class Form1 : Form
    {
        public static int FRAME_BUFFER_WIDTH = 128;
        public static int FRAME_BUFFER_HEIGHT = 128;

        public static int FRAME_BUFFER_LENGTH {
            get { return (FRAME_BUFFER_WIDTH * FRAME_BUFFER_HEIGHT) * 3; }
        }

        ICSharpCode.AvalonEdit.TextEditor textEditor;
        FCPU.FCPU CPU = new FCPU.FCPU(new FCPUState(2048 + FRAME_BUFFER_LENGTH));

        public Form1() {
            InitializeComponent();
            AddEditorControl();

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


        XshdSyntaxDefinition xshd;
        private void AddEditorControl()
        {
            textEditor = new ICSharpCode.AvalonEdit.TextEditor();
            textEditor.ShowLineNumbers = true;
            textEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            textEditor.FontSize = 12.75f;
            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;

            if (File.Exists("FUASM.xshd"))
            {
                Stream xshd_stream = File.OpenRead("FUASM.xshd");
                XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
                xshd = HighlightingLoader.LoadXshd(xshd_reader);
                updateStandardParametersList(); // Runtime load in OPCodes
                var custom = HighlightingLoader.Load(xshd, HighlightingManager.Instance);

                // Apply the new syntax highlighting definition.
                textEditor.SyntaxHighlighting = custom;
                xshd_reader.Close();
                xshd_stream.Close();
            }

            //Host the WPF AvalonEdiot control in a Winform ElementHost control
            elementHost1.Child = textEditor;
        }

        void updateStandardParametersList()
        {
            XshdKeywords newKeyWords = new XshdKeywords();
            newKeyWords.ColorReference = new XshdReference<XshdColor>(null, "OpCodes");

            (from item in FCPU.FCPU.OpCodes
             select item.Value.InsName).ToList().ForEach(newKeyWords.Words.Add);

            XshdRuleSet mainRuleSet = xshd.Elements.OfType<XshdRuleSet>().Where(o => string.IsNullOrEmpty(o.Name)).First();
            mainRuleSet.Elements.Add(newKeyWords);
        }

        CompletionWindow completionWindow;
        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            var current_word = TextUtilities.GetNextCaretPosition(textEditor.Document, textEditor.CaretOffset, LogicalDirection.Backward, CaretPositioningMode.WordStart);
            // Open code completion after the user has pressed dot:
            completionWindow = new CompletionWindow(textEditor.TextArea);
            current_word = Math.Max(0, current_word);
            completionWindow.StartOffset = current_word;
            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
            var valid_options = (from item in FCPU.FCPU.OpCodes where item.Value.InsName.StartsWith(    textEditor.Text.Substring(  current_word,
                                                                                                                                    Math.Max(0, textEditor.CaretOffset - current_word)))
                                 select new MyCompletionData(item.Value.InsName, item.Value.DocString)).ToList();
            if (valid_options.Count > 0)
            {
                valid_options.ForEach(data.Add);
                completionWindow.Show();
                completionWindow.Closed += delegate
                {
                    completionWindow = null;
                };
            }
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // Do not set e.Handled=true.
            // We still want to insert the character that was typed.
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            CPU.State = new FCPUState(2048 + FRAME_BUFFER_LENGTH);
            BasicParser.LoadCodeV2(CPU, textEditor.Text);
            UpdateDisplays();
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
                    if(MemoryLoc.Ptr < 2048)
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

                UpdateDisplays();
                UpdateFrameBuffer();
            }
            catch (Exception E) {
                ConsoleColor Old = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Runtime Error: " + E.Message);
                Console.ForegroundColor = Old;
            }
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
