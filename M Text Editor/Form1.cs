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

namespace M_Text_Editor
{
    public partial class Form1 : Form
    {
        String file;
        String filelines;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("resized"+e);
            textArea.Width = this.Width;
            textArea.Height = this.Height;
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
                while (text != null)
                {
                    filelines += text + Environment.NewLine;
                    text = _file.ReadLine();
                }
                textArea.Text = filelines;
                _file.Close();
            }

        }

        private void saveFile() {
            if (file != null)
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
            if (filelines != null)
            {
                String tss = textArea.Text;
                if (!(textArea.Text.Contains(filelines)))
                {
                    string message = "It is recommended to save everytime. Do you want to save?";
                    string caption = "About M Text Editor";
                    MessageBoxButtons _buttons = MessageBoxButtons.YesNo;
                    var result = MessageBox.Show(message, caption, _buttons);
                    if (result == DialogResult.Yes)
                    {
                        saveFile();
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }

            }
            else {
                saveFile();
            }
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "This app is created by Manoj Paramsetti\n\nVersion 1.0.1";
            string caption = "About M Text Editor";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            var result = MessageBox.Show(message, caption);
        }
    }
}
