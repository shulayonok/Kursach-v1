using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kursach_v1
{
    public partial class ModifyTableForm : Form
    {
        private string pattern = ": type = ";
        private bool isCorrect;
        private int index;
        private string temp;
        private string[] mas;
        internal int numType = -1;
        public string modifyColumn;
        public string modifyValues;
        DataRow oneRow;
        public ModifyTableForm()
        {
            InitializeComponent();
            textBoxColumnName.Enabled = false;
            richTextBoxValues.Enabled = false;  
            ApplyBtn.Enabled = false;   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.isModified = true;
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
            isCorrect = false;
            if (textBoxColumnName.Text == "" && !checkBox3.Checked)
            {
                MessageBox.Show("Please, enter the name of the column", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (richTextBoxValues.Text == "" && checkBox1.Checked)
            {
                MessageBox.Show("Please, enter the values of the column", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkBox1.Checked)
            {
                if (!Regex.IsMatch(textBoxColumnName.Text, pattern))
                {
                    MessageBox.Show("Specify data type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    temp = textBoxColumnName.Text;
                    index = temp.IndexOf(pattern);
                    temp = temp.Replace(pattern, " ");
                    for (int j = 0; j < MainForm.types.Length; j++)
                    {
                        if (temp.Substring(index + 1) == MainForm.types[j])
                        {
                            isCorrect = true;
                            numType = j;
                            break;
                        }
                        else if (j == MainForm.types.Length - 1)
                        {
                            isCorrect = false;
                            MessageBox.Show("Incorrect data type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    // проверка на вводимый тип данных и на то, что вводимые значения не пустые
                    if (isCorrect && isFilling(richTextBoxValues.Text))
                    {
                        modifyColumn = textBoxColumnName.Text.Substring(0, textBoxColumnName.Text.IndexOf(pattern)).Trim();
                        MainForm.modifyOption = 2;
                        MainForm.columnnames += modifyColumn + '\n';
                        MessageBox.Show("Table modification was successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm.isModified = true;
                        modifyValues = richTextBoxValues.Text; 
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect data type or incorrect amount of data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (checkBox2.Checked)
            {
                mas = MainForm.columnnames.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                temp = textBoxColumnName.Text.Trim();
                for (int i = 0; i < mas.Length; i++)
                {
                    if (Regex.IsMatch(temp, mas[i]))
                    {
                        DialogResult result = MessageBox.Show("Are you sure?", "Confirm the removing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            MainForm.modifyOption = 1;
                            modifyColumn = temp;
                            modifyValues = "";
                            MainForm.columnnames = MainForm.columnnames.Replace(temp + '\n', "");
                            MainForm.isModified = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Table modification was not successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                    else if(i == mas.Length - 1)
                        MessageBox.Show("Incorrect name of column!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (isFilling_2(richTextBoxValues.Text))
                {
                    if(MainForm.isEqual(oneRow))
                    {
                        MainForm.modifyOption = 3;
                        MessageBox.Show("Table modification was successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainForm.isModified = true;
                        modifyValues = richTextBoxValues.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private bool isFilling(string names)
        {
            string[] Rows = names.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            object obj;
            if (Rows.Length != MainForm.table.Rows.Count)
                return false;
            for (int i = 0; i < Rows.Length; i++)
            {
                try
                {
                    Rows[i] = Rows[i].Trim();
                    switch (numType)
                    {
                        case 0:
                            obj = Rows[i];
                            break;
                        case 1:
                            obj = Convert.ToInt32(Rows[i]);
                            break;
                        case 2:
                            Rows[i] = Rows[i].Replace(".", ",");
                            obj = Convert.ToDouble(Rows[i]);
                            break;
                        case 3:
                            obj = Convert.ToBoolean(Rows[i]);
                            break;
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

        private bool isFilling_2(string names)
        {
            oneRow = MainForm.table.NewRow();
            string[] Rows = names.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (Rows.Length > 1)
                return false;
            string[] temp;
            object[] mas = { };
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
            oneRow.ItemArray = mas; 
            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                textBoxColumnName.Enabled = true;
                richTextBoxValues.Enabled = true;
                ApplyBtn.Enabled = true;
            }
            else if (checkBox1.Checked && checkBox2.Checked || checkBox1.Checked && checkBox3.Checked)
            {
                textBoxColumnName.Enabled = true;
                richTextBoxValues.Enabled = true;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                ApplyBtn.Enabled = true;
            }
            else if (!checkBox1.Checked && checkBox2.Checked || !checkBox1.Checked && checkBox3.Checked) { }
            else
            {
                textBoxColumnName.Enabled = false;
                richTextBoxValues.Enabled = false;
                ApplyBtn.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && checkBox2.Checked && !checkBox3.Checked)
            {
                textBoxColumnName.Enabled = true;
                richTextBoxValues.Enabled = false;
                ApplyBtn.Enabled = true;
            }
            else if (checkBox1.Checked && checkBox2.Checked || checkBox3.Checked && checkBox2.Checked)
            {
                textBoxColumnName.Enabled = true;
                richTextBoxValues.Enabled = false;
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                ApplyBtn.Enabled = true;
            }
            else if (checkBox1.Checked && !checkBox2.Checked || checkBox3.Checked && !checkBox2.Checked) { }
            else
            {
                textBoxColumnName.Enabled = false;
                richTextBoxValues.Enabled = false;
                ApplyBtn.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked)
            {
                textBoxColumnName.Enabled = false;
                richTextBoxValues.Enabled = true;
                ApplyBtn.Enabled = true;
            }
            else if (checkBox1.Checked && checkBox3.Checked || checkBox3.Checked && checkBox2.Checked)
            {
                textBoxColumnName.Enabled = false;
                richTextBoxValues.Enabled = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                ApplyBtn.Enabled = true;
            }
            else if (checkBox1.Checked && !checkBox3.Checked || !checkBox3.Checked && checkBox2.Checked) { }
            else
            {
                textBoxColumnName.Enabled = false;
                richTextBoxValues.Enabled = false;
                ApplyBtn.Enabled = false;
            }
        }
    }
}
