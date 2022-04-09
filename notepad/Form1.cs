using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace notepad
{
    public partial class Form1 : Form
    {

        public string filename;
        public bool isFileChanged;
        public System.Drawing.FontStyle fs = FontStyle.Regular;
        public FontSettings fonts;



        public Form1()
        {
            InitializeComponent();
            Init();
            
        }

        public void Init()
        {
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
        }

        private void newdocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            filename = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "TEXT|*.txt";
                openFileDialog.Title = "chose file";
                SaveUnsavedFile();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamReader st = new StreamReader(openFileDialog.FileName);
                        textBox1.Text = st.ReadToEnd();
                        st.Close();
                        filename = openFileDialog.FileName;

                    }
                    catch
                    {
                        MessageBox.Show("not posible to open the file");
                    }
                }
            }
        }

        private void saveDoc(string _filename)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                if (_filename == "")
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _filename = saveFileDialog.FileName;
                    }
                }
                try
                {
                    StreamWriter sw = new StreamWriter(_filename);
                    sw.WriteLine(textBox1.Text);
                    sw.Close();
                    filename = _filename;
                    isFileChanged = false;

                }
                catch
                {
                    MessageBox.Show("not posible to save the file");
                }
            }
            UpdateTextWithTitle();
        }


        public void Save(object sender, EventArgs e)
        {
            saveDoc(filename);
        }

        public void SaveAs(object sender, EventArgs e)
        {
            saveDoc("");
        }

        private void OnTextChanged()
        {
            if (!isFileChanged)
            {
                this.Text = this.Text.Replace('*', ' ');
                isFileChanged = true;
                this.Text = "*" + this.Text;
            }
        }

        public void UpdateTextWithTitle()
        {
            if (filename != "")
            {
                this.Text = filename + " - Notepad";
            }
            else this.Text = "NoName - Notepad";
        }

        public void SaveUnsavedFile()
        {
            if (!isFileChanged)
            {
                DialogResult result = MessageBox.Show("save changes", "save file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    saveDoc(filename);
                }
            }
        }

        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        public void CutText()
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
        }

        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length-textBox1.SelectionStart);
            Clipboard.GetText();
        }

        private void OnClickCopy(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnClickCut(object sender, EventArgs e)
        {
            CutText();  
        }

        private void OnClickPaste(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUnsavedFile();
        }

        private void OnFontClick(object sender, EventArgs e)
        {
            fonts = new FontSettings();
            fonts.Show();
        }

        private void OnFocus(object sender, EventArgs e)
        {
            //if (fonts != null)
            //{
            //    fs = fonts.fs;
            //}
        }
    }
}
