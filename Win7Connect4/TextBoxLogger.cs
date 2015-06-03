using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Win7Connect4
{
    class TextBoxLogger : TextWriter
    {
        TextBlock textBox = null;

         public TextBoxLogger(TextBlock output)
        {
            textBox = output;
        }
 
        public override void Write(char value)
        {
            base.Write(value);
            textBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBox.Text = textBox.Text + (value.ToString());
            }));
        }
 
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
