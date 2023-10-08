using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kursach_v1
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.isKeying = true;
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

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            if (textBoxKEY.Text != "")
            {
                MainForm.KEY = textBoxKEY.Text;
                MainForm.isKeying = true;
                this.Close();
            }
            else
                MessageBox.Show("Please, enter the key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
