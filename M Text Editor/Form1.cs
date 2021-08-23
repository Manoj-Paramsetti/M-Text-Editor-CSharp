using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Documents;
using Microsoft.VisualBasic;

namespace M_Text_Editor
{
    public partial class Form1 : Form
    {
        String file = "";
        String filelines;
        String formName = "Untitled - M Text Editor";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("resized"+e);
            textArea.Width = this.Width - 18;
            textArea.Height = this.Height - 66;
        }

        private void openFile() {
            OpenFileDialog v1 = new OpenFileDialog();

            if (v1.ShowDialog() == DialogResult.OK)
            {
                file = v1.FileName;
                Console.WriteLine(file);
                StreamReader _file = new StreamReader(file);
                String text = _file.ReadLine();
                textArea.Text = "";
                filelines = "";
                formName = file + " - M Text Editor";
                this.Text = formName;
                
                while (text != null)
                {
                    filelines += text + "\r\n";
                    text = _file.ReadLine();
                }
                textArea.Text = filelines;
                _file.Close();
            }

        }

        private void saveFile() {
            if (file != "")
            {
                File.WriteAllLines(file, textArea.Text.Split(Environment.NewLine.ToCharArray()));
            }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    File.WriteAllLines(saveFileDialog1.FileName, textArea.Text.Split(Environment.NewLine.ToCharArray()));
                    formName = saveFileDialog1.FileName + " - M Text Editor";
                    this.Text = formName;
                }
            }
        }

        private void saveFileAs() {
            SaveFileDialog save = new SaveFileDialog();
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                File.WriteAllLines(saveFileDialog1.FileName, textArea.Text.Split(Environment.NewLine.ToCharArray()));
            }
        }

        private void changeFont() {
            DialogResult font = fontDialog1.ShowDialog();
            if (font == DialogResult.OK)
            {
                textArea.Font = fontDialog1.Font;
            }
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeFont();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                openFile();
            }
            else if (e.Control && e.KeyCode == Keys.S && !e.Shift)
            {
                saveFile();
            }
            else if (e.Alt && e.KeyCode == Keys.S && e.Shift)
            {
                saveFileAs();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                changeFont();
            }
            else if (e.Control && e.KeyCode == Keys.Oemplus) {
                var currentSize = textArea.Font.Size;
                currentSize += 2.0F;
                textArea.Font = new Font(textArea.Font.Name, currentSize, textArea.Font.Style, textArea.Font.Unit );
            }
            else if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                var currentSize = textArea.Font.Size;
                currentSize -= 2.0F;
                textArea.Font = new Font(textArea.Font.Name, currentSize, textArea.Font.Style, textArea.Font.Unit);
            }
        }

        private void increaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentSize = textArea.Font.Size;
            currentSize += 2.0F;
            textArea.Font = new Font(textArea.Font.Name, currentSize, textArea.Font.Style, textArea.Font.Unit);
        }

        private void decreaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentSize = textArea.Font.Size;
            currentSize -= 2.0F;
            textArea.Font = new Font(textArea.Font.Name, currentSize, textArea.Font.Style, textArea.Font.Unit);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = formName;
        }

        private void aboutCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form about = new Form();
            string message = "   This app is created by Manoj Paramsetti\n\n" +
                             "                           Version 1.0.1";
            string caption = "About - M Text Editor";

            var result = MessageBox.Show(message, caption);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Manoj-Paramsetti/M-Text-Editor-CSharp/wiki");
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textArea.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textArea.Redo();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newform = new Form1();
            newform.Show();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (filelines != null)
            {
                var first = filelines;
                var second = textArea.Text;
                if (!(second.Equals(first)))
                {
                    string message = "It is recommended to save everytime. Do you want to save?";
                    string caption = "Exit";
                    MessageBoxButtons _buttons = MessageBoxButtons.YesNoCancel;
                    var result = MessageBox.Show(message, caption, _buttons);
                    if (result == DialogResult.Yes)
                    {
                        saveFile();
                        e.Cancel = false;
                        result = DialogResult.No;
                    }

                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = (result == DialogResult.Cancel);
                    }        
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else if (file == "" && textArea.Text != null)
            {

                string message = "It's recommended to save everytime. Do you want to save?";
                string caption = "Exit";
                MessageBoxButtons _buttons = MessageBoxButtons.YesNoCancel;
                var result = MessageBox.Show(message, caption, _buttons);
                if (result == DialogResult.Yes)
                {
                    saveFile();
                    e.Cancel = false;
                    result = DialogResult.No;
                }

                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = (result == DialogResult.Cancel);
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
