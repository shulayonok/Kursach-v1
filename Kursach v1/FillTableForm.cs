using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kursach_v1
{
    public partial class FillTableForm : Form
    {
        public FillTableForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.isFilled = true;
            this.Close();
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

        private bool Proverka()
        {
            string[] Rows = textBoxNames.Text.Split('\n');
            string[] temp;
            foreach (string Row in Rows)
            {
                temp = Row.Split(';');
                if (temp.Length != MainForm.numTypes.Length)
                {
                    MessageBox.Show("Number of rows is not equal number of fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;    
        }

        private bool isFilling(string names)
        {
            string[] Rows = names.Split('\n');
            string[] temp;
            object[] mas = {};
            for (int i = 0; i < Rows.Length; i++)
            {
                temp = Rows[i].Split(';');
                mas = new object[temp.Length];
                try
                {
                    for (int j = 0; j < temp.Length; j++)
                    {
                        temp[j] = temp[j].Trim();
                        switch (MainForm.numTypes[j])
                        {
                            case 0:
                                mas[j] = temp[j];
                                break;
                            case 1:
                                mas[j] = Convert.ToInt32(temp[j]);
                                break;
                            case 2:
                                temp[j] = temp[j].Replace(".", ",");
                                mas[j] = Convert.ToDouble(temp[j]);
                                break;
                            case 3:
                                mas[j] = Convert.ToBoolean(temp[j]);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                { 
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            if (textBoxNames.Text == "")
                MessageBox.Show("Incorrect fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(Proverka())
            {
                if (isFilling(textBoxNames.Text))
                {
                    MainForm.rownames = textBoxNames.Text;
                    MainForm.isFilled = true;
                    MessageBox.Show("Table filling was successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
