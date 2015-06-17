using ICSharpCode.AvalonEdit.CodeCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace FCPUDebugger
{
    public class MyCompletionData : ICompletionData
    {
        public MyCompletionData(string text, string desc)
        {
            this.Text = text;
            this.Desc = desc;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }
        public string Desc { get; private set; }

        

        public object Description
        {
            get { return this.Text + ": " + this.Desc; }
        }

        public double Priority
        {
            get
            {
                return 0;
            }
        }

        public object Content
        {
            get
            {
                 return this.Text; 
            }
        }

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }
    }
}
