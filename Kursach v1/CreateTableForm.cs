using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kursach_v1
{
    public partial class CreateTableForm : Form
    {
        private int index;
        private string pattern = ": type = ";
        bool isCorrect;
        string[] strings;
        public CreateTableForm()
        {
            InitializeComponent();
        }

        Point p;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - p.X;
                this.Top += e.Y - p.Y;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.isCreated = true;
            this.Close();
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            isCorrect = false;
            if (textBoxTableName.Text == "" || richTextBoxNames.Text == "")
                MessageBox.Show("Incorrect fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!Regex.IsMatch(richTextBoxNames.Text, pattern))
            {
                MessageBox.Show("Specify data type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                strings = richTextBoxNames.Text.Split('\n');
                for (int i = 0; i < strings.Length; i++)
                {
                    if (!Regex.IsMatch(strings[i], pattern))
                    {
                        MessageBox.Show("Specify data type or correct syntax mistakes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isCorrect = false;
                        goto exit;
                    }
                    index = strings[i].IndexOf(pattern);
                    strings[i] = strings[i].Replace(pattern, " ");
                    for (int j = 0; j < MainForm.types.Length; j++)
                    {
                        if (strings[i].Substring(index + 1) == MainForm.types[j])
                        {
                            isCorrect = true;
                            break;
                        }
                        else if (j == MainForm.types.Length - 1)
                        {
                            isCorrect = false;
                            MessageBox.Show("Incorrect data type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            goto exit;
                        }
                    }
                }
                exit: if (isCorrect)
                {
                    MainForm.name = textBoxTableName.Text;
                    MainForm.columnnames = richTextBoxNames.Text;
                    MessageBox.Show("Table creation was successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm.isCreated = true;
                    this.Close();
                }
            }
        }
    }
}
