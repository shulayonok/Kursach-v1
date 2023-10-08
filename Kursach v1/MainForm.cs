using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace Kursach_v1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            TableGrid.Hide();
            ResultGrid.Hide();
            ApplyBtn.Enabled = false;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            TableGrid.AllowUserToAddRows = false;
            ResultGrid.AllowUserToAddRows = false;
            errorLabel.Hide();
        }

        static internal bool isCreated = false;
        static internal bool isModified = false;
        static internal bool isFilled = false;
        static internal bool isKeying = false;
        private bool isWorking;
        private bool option;

        static internal string name = "";
        static internal string columnnames;
        private string savedTable;
        static internal string rownames = "";

        static internal DataTable table = null;
        static private DataTable resultTable = null;
        static internal DataRow ROW = null;

        internal static string[] types = {"string", "int", "double", "bool"};
        internal static int[] numTypes;
        private string[] openedTable;
        private string[] operators = {">", "<", ">=", "<=", "==", "!=" };

        private string pattern = "select ";
        private string pattern2 = "where ";
        static internal string KEY = "";
        private char symbol = ';';

        int oper;
        int index, index2;
        static public int modifyOption = -1;

        private void button1_Click(object sender, EventArgs e)
        {
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            if (Keying())
            {
                // получаем выбранный файл
                string filename = openFileDialog1.FileName;
                // читаем файл в строку
                string fileText = System.IO.File.ReadAllText(filename);
                // получаем ключ
                byte[] key = Encoding.Default.GetBytes(KEY);
                RC4 decoder = new RC4(key);
                // расшифровываем
                byte[] encryptedBytes = Encoding.Default.GetBytes(fileText);
                byte[] decryptedBytes = decoder.Decode(encryptedBytes, encryptedBytes.Length);
                fileText = Encoding.Default.GetString(decryptedBytes);
                // открываем таблицу
                openedTable = fileText.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (openedTable.Length <= 1)
                {
                    MessageBox.Show("Incorrect key or damaged data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    table = null;
                }
                else
                {
                    try
                    {
                        table = new DataTable();
                        string[] temp;
                        for (int i = 0; i < openedTable.Length; i++)
                        {
                            if (i == 0)
                            {
                                table.TableName = openedTable[i].Trim();
                                name = openedTable[i].Trim();
                            }
                            else if (i == 1)
                            {
                                temp = openedTable[i].Split(new char[1] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                numTypes = new int[temp.Length];
                                for (int j = 0; j < temp.Length; j++)
                                {
                                    numTypes[j] = Convert.ToInt32(temp[j].Trim());
                                }
                            }
                            else if (i == 2)
                            {
                                temp = openedTable[i].Split(new char[1] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int j = 0; j < temp.Length; j++)
                                {
                                    columnnames += temp[j] + '\n';
                                }
                                for (int j = 0; j < temp.Length; j++)
                                {
                                    switch (numTypes[j])
                                    {
                                        case 0:
                                            table.Columns.Add(temp[j].Trim(), typeof(string));
                                            break;
                                        case 1:
                                            table.Columns.Add(temp[j].Trim(), typeof(int));
                                            break;
                                        case 2:
                                            table.Columns.Add(temp[j].Trim(), typeof(double));
                                            break;
                                        case 3:
                                            table.Columns.Add(temp[j].Trim(), typeof(bool));
                                            break;
                                    }
                                }
                                TableGrid.DataSource = table;
                                TableGrid.Show();
                            }
                            else
                            {
                                FillTable(openedTable[i]);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Incorrect key or damaged data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        table = null;
                    }
                }
                if(table != null)
                {
                    ApplyBtn.Enabled = true;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (table != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                if(Keying())
                {
                    // получаем ключ
                    byte[] key = Encoding.Default.GetBytes(KEY);
                    RC4 encoder = new RC4(key);
                    // получаем выбранный файл
                    string filename = saveFileDialog1.FileName;
                    savedTable = table.TableName + "\n";
                    // получаем текст таблицы
                    for (int i = 0; i < numTypes.Length; i++)
                    {
                        savedTable += "\t" + numTypes[i].ToString();
                    }
                    savedTable += "\n";
                    foreach (DataColumn column in table.Columns)
                    {
                        savedTable += "\t" + column.ColumnName;
                    }
                    savedTable += "\n";
                    foreach (DataRow row in table.Rows)
                    {
                        // получаем все ячейки строки
                        var cells = row.ItemArray;
                        foreach (object cell in cells)
                            savedTable += "\t" + cell;
                        savedTable += "\n";
                    }
                    // шифруем текст по ключу
                    byte[] tableBytes = Encoding.Default.GetBytes(savedTable);
                    byte[] result = encoder.Encode(tableBytes, tableBytes.Length);
                    savedTable = Encoding.Default.GetString(result);
                    // сохраняем текст в файл
                    System.IO.File.WriteAllText(filename, savedTable);
                    MessageBox.Show("Table successfully saved!");
                }
            }
            else
            {
                MessageBox.Show("Please, create or open any table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Creation ()
        {
            CreateTableForm createtableform = new CreateTableForm();
            createtableform.ShowDialog();
            while (!isCreated) ;
            if (name != "")
                CreateTable(columnnames);
            else
                MessageBox.Show("Something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool Keying()
        {
            KeyForm keyform = new KeyForm();
            keyform.ShowDialog();
            while (!isKeying) ;
            if (KEY != "")
                return true;
            else
            {
                MessageBox.Show("Something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Filling()
        {
            FillTableForm filltableform = new FillTableForm();
            filltableform.ShowDialog();
            while (!isFilled);
            if(rownames != "")
                FillTable(rownames);
            else
                MessageBox.Show("Something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Modification()
        {
            isModified = false;
            modifyOption = -1;
            ModifyTableForm modifytableform = new ModifyTableForm();
            modifytableform.ShowDialog();
            while(!isModified);
            if(modifyOption != -1)
                ModifyTable(modifytableform.modifyColumn, modifytableform.modifyValues, modifyOption, modifytableform.numType);
            else
                MessageBox.Show("Something wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (table == null)
            {
                name = "";
                rownames = "";
                Creation();
            }
            else
            {
                DialogResult result = MessageBox.Show("The previous table will be deleted. Are you sure?", "Confirm the creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    name = "";
                    rownames = "";
                    Creation();
                }
                else
                    MessageBox.Show("Table creation was not successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(table != null)
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Confirm the removing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    table = null;
                    resultTable = null;
                    name = "";
                    rownames = "";
                    MessageBox.Show("Table removing was successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TableGrid.DataSource = table;
                    ResultGrid.DataSource = resultTable;
                    TableGrid.Update();
                    ResultGrid.Update();
                    ApplyBtn.Enabled = false;
                }
                else
                    MessageBox.Show("Table removing was not successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Table has not been created!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(table != null)
            {
                Filling();
                ApplyBtn.Enabled = true;
            }
            else
                MessageBox.Show("Table has not been created!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (table != null)
            {
                Modification();
            }
            else
                MessageBox.Show("Table has not been created!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateTable(string names)
        {
            table = new DataTable();
            table.TableName = name;
            columnnames = "";
            int index, num;
            string[] Columnnames = names.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            numTypes = new int[Columnnames.Length];
            for (int i = 0; i < Columnnames.Length; i++)
            {
                index = Columnnames[i].IndexOf(": type = ");
                Columnnames[i] = Columnnames[i].Replace(": type = ", " ");
                string name = Columnnames[i].Substring(0, index);
                string type = Columnnames[i].Substring(index + 1);
                num = CorrectType(type);
                numTypes[i] = num;
                switch(num)
                {
                    case 0:
                        table.Columns.Add(name, typeof(string));
                        break;
                    case 1:
                        table.Columns.Add(name, typeof(int));
                        break;
                    case 2:
                        table.Columns.Add(name, typeof(double));
                        break;
                    case 3:
                        table.Columns.Add(name, typeof(bool));
                        break;
                }
                columnnames += name + '\n';
            }
            TableGrid.DataSource = table;
            TableGrid.Show();
        }

        private int CorrectType(string type)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (type == types[i])
                    return i;
            }
            return -1;
        }

        private void FillTable(string names)
        {
            string[] Rows = names.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] temp;
            object[] mas;
            for (int i = 0; i < Rows.Length; i++)
            {
                temp = Rows[i].Split(new char[2]{';', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                mas = new object[temp.Length];

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();
                    switch (numTypes[j])
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

                table.Rows.Add(mas);
            }
            TableGrid.DataSource = table;
            TableGrid.Update();
        }

        private void ModifyTable(string column, string values, int option, int type)
        {
            switch(option)
            {
                case 1:
                    foreach(DataColumn c in table.Columns)
                    {
                        if (c.ColumnName == column)
                        {
                            table.Columns.Remove(c);
                            break;
                        }
                    }
                    break;
                case 2:
                    switch (type)
                    {
                        case 0:
                            table.Columns.Add(column, typeof(string));
                            break;
                        case 1:
                            table.Columns.Add(column, typeof(int));
                            break;
                        case 2:
                            table.Columns.Add(column, typeof(double));
                            break;
                        case 3:
                            table.Columns.Add(column, typeof(bool));
                            break;
                    }
                    Array.Resize(ref numTypes, numTypes.Length + 1);
                    numTypes[numTypes.Length - 1] = type;
                    string[] temp = values.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    int count = 0;
                    foreach (DataRow r in table.Rows)
                    {
                        switch (type)
                        {
                            case 0:
                                r[column] = temp[count];
                                break;
                            case 1:
                                r[column] = Convert.ToInt32(temp[count]);
                                break;
                            case 2:
                                temp[count] = temp[count].Replace(".", ",");
                                r[column] = Convert.ToDouble(temp[count]);
                                break;
                            case 3:
                                r[column] = Convert.ToBoolean(temp[count]);
                                break;
                        }
                        count++;
                    }
                    break;
                case 3:
                    table.Rows.Remove(ROW);
                    break;
            }
            TableGrid.DataSource = table;
            TableGrid.Update();
        }
       
        static internal bool isEqual(DataRow r)
        {
            object[] one;
            object[] two = r.ItemArray;
            bool correct = false;

            foreach (DataRow row in table.Rows)
            {
                correct = true;
                one = row.ItemArray;
                for (int i = 0; i < two.Length; i++)
                {
                    if (!one[i].Equals(two[i]))
                    {
                        correct = false;
                        break;
                    }
                    else if (i == two.Length - 1)
                    {
                        ROW = row;
                        return correct;
                    }
                        
                }
            }
            return correct;
        }

        private bool isString(ref string var)
        {
            if (var.IndexOf('\"') == 0 && var.Substring(1).IndexOf('\"') + 1 == var.Length - 1)
            {
                var = var.Substring(1, var.Length - 2);
                return true;
            }
            return false;
        }

        private bool DoCommand(string columns, bool option, string expression, int oper, int str_num)
        {
            resultTable = new DataTable();
            string[] mas_columns = columns.Split(new char[1] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            object[] mas = new object[mas_columns.Length];
            foreach(string column in mas_columns)
                resultTable.Columns.Add(column);    
            // если опция с where
            if (option)
            {
                string var;
                foreach (DataColumn c in table.Columns)
                {
                    if (Regex.IsMatch(expression, c.ColumnName))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            string temp = row[c].ToString();
                            var = expression.Substring(expression.IndexOf(operators[oper]) + 2).Trim();
                            if (!isString(ref var))
                            {
                                if (int.TryParse(temp, out var res1) && int.TryParse(var, out var res2))
                                {
                                    switch (oper)
                                    {
                                        case 0:
                                            if (res1 > res2)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 1:
                                            if (res1 < res2)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 2:
                                            if (res1 >= res2)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 3:
                                            if (res1 <= res2)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 4:
                                            if (res1 == res2)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 5:
                                            if (res1 != res2)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                    }
                                }
                                else if (double.TryParse(temp, out var res3) && double.TryParse(var, out var res4))
                                {
                                    switch (oper)
                                    {
                                        case 0:
                                            if (res3 > res4)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 1:
                                            if (res3 < res4)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 2:
                                            if (res3 >= res4)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 3:
                                            if (res3 <= res4)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 4:
                                            if (res3 == res4)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 5:
                                            if (res3 != res4)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                    }
                                }
                                else if (bool.TryParse(temp, out var res5) && bool.TryParse(var, out var res6))
                                {
                                    switch (oper)
                                    {
                                        case 4:
                                            if (res5 == res6)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        case 5:
                                            if (res5 != res6)
                                                Insertion(mas_columns, row, mas);
                                            break;
                                        default:
                                            Error(str_num, 3);
                                            return false;
                                    }
                                }
                                else
                                {
                                    Error(str_num, 4);
                                    return false;
                                }
                            }
                            else
                            {
                                switch (oper)
                                {
                                    case 4:
                                        if (temp == var)
                                            Insertion(mas_columns, row, mas);
                                        break;
                                    case 5:
                                        if (temp != var)
                                            Insertion(mas_columns, row, mas);
                                        break;
                                    default:
                                        Error(str_num, 3);
                                        return false;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                foreach (DataRow row in table.Rows)
                    Insertion(mas_columns, row, mas);
            }
            ResultGrid.DataSource = resultTable;
            ResultGrid.Show();
            return true;
        }

        private void Insertion(string[] m, DataRow r, object[] M)
        {
            for (int i = 0; i < m.Length; i++)
            {
                M[i] = r[m[i]];
            }
            resultTable.Rows.Add(M);
        }

        private void Error(int i, int format)
        {
            if (format == 1)
                errorLabel.Text = string.Format("Syntax error!\nString number: {0}", i + 1);
            else if (format == 2)
                errorLabel.Text = string.Format("Field error!\nString number: {0}", i + 1);
            else if (format == 3)
                errorLabel.Text = string.Format("Operator error!\nString number: {0}", i + 1);
            else if (format == 4)
                errorLabel.Text = string.Format("Type error!\nString number: {0}", i + 1);
            errorLabel.Show();
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            errorLabel.Hide();
            isWorking = false;
            string temp = null;
            string columns = "";
            oper = -1;
            option = false;
            string[] commands = richTextBox1.Text.Split('\n');
            for (int i = 0; i < commands.Length; i++)
            {
                // если нет команд
                if (commands[i] == "")
                    continue;
                // если строка содержит "select"
                else if (Regex.IsMatch(commands[i], pattern))
                {
                    // select должен быть на первом месте
                    if (commands[i].IndexOf(pattern) == 0)
                    {
                        // вычисляем индекс ";"
                        index = commands[i].IndexOf(symbol);
                        // если есть ";"
                        if (index != -1)
                        {
                            // ";" должен стоять в конце строки
                            if (index == commands[i].Length - 1)
                                continue;
                            else
                            {
                                Error(i, 1);
                                goto end;
                            }
                        }
                        else
                        {
                            // если есть "where"
                            if (Regex.IsMatch(commands[i], pattern2))
                            {
                                // проверяем в строке наличие логического оператора и чтобы он стоял после where
                                for (int j = 0; j < operators.Length; j++)
                                {
                                    if (Regex.IsMatch(commands[i], operators[j]))
                                    {
                                        oper = j;
                                        option = true;
                                    }
                                }
                                if(!option)
                                {
                                    Error(i, 1);
                                    goto end;
                                }
                            }
                            // проходим по всем колонкам и ищем соответствие
                            foreach (DataColumn column in table.Columns)
                            {
                                // с where
                                if (option)
                                {
                                    index2 = commands[i].IndexOf(pattern2);
                                    if (Regex.IsMatch(commands[i].Substring(0, index2), column.ColumnName))
                                    {
                                        index = commands[i].IndexOf(column.ColumnName);
                                        temp = commands[i].Substring(index2 + pattern2.Length);
                                        if (index < index2)
                                        {
                                            columns += column.ColumnName + '\t';   
                                        }
                                        else
                                        {
                                            Error(i, 1);
                                            goto end;
                                        }
                                    }
                                }
                                // без where
                                else
                                {
                                    if(Regex.IsMatch(commands[i], column.ColumnName))
                                    {
                                        columns += column.ColumnName + '\t';
                                    }
                                }
                            }
                        }
                        if (columns != "")
                        {
                            if(DoCommand(columns, option, temp, oper, i))
                                errorLabel.Hide();
                            isWorking = true;
                            break;
                        }
                        if (!isWorking)
                        {
                            Error(i, 2);
                        }
                    }
                }
                else
                {
                    Error(i, 1);
                }
            end: break;
            }
        }
    }
}
