using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace MeteoShedule
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "meteoSheduleDataSet.Полный_График_Работы". При необходимости она может быть перемещена или удалена.
            this.полный_График_РаботыTableAdapter.Fill(this.meteoSheduleDataSet.Полный_График_Работы);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(66, 81, 156, 1);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(42, 45, 129);
            dataGridView1.BackgroundColor = Color.FromArgb(42, 45, 129);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("PT Sans", 12, FontStyle.Regular);
           DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("PT Sans", 12, FontStyle.Bold);
            columnHeaderStyle.BackColor = Color.FromArgb(66, 81, 156);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            Load_Items();
            progressBar1.ForeColor = Color.White;
            progressBar1.Visible = false;
            contextMenuStrip1.BackColor = Color.FromArgb(183, 211, 255);
            contextMenuStrip1.ForeColor = Color.FromArgb(42, 45, 129);
            удалитьToolStripMenuItem.BackColor = Color.FromArgb(183, 211, 255);
            удалитьToolStripMenuItem.ForeColor = Color.FromArgb(42, 45, 129);
            редактироватьToolStripMenuItem.BackColor = Color.FromArgb(183, 211, 255);
            редактироватьToolStripMenuItem.ForeColor = Color.FromArgb(42, 45, 129);
            contextMenuStrip2.BackColor = Color.FromArgb(183, 211, 255);
            contextMenuStrip2.ForeColor = Color.FromArgb(42, 45, 129);
            отменитьToolStripMenuItem.BackColor = Color.FromArgb(183, 211, 255);
            отменитьToolStripMenuItem.ForeColor = Color.FromArgb(42, 45, 129);
            сохранитьToolStripMenuItem.BackColor = Color.FromArgb(183, 211, 255);
            сохранитьToolStripMenuItem.ForeColor = Color.FromArgb(42, 45, 129);
            редактироватьToolStripMenuItem1.BackColor = Color.FromArgb(183, 211, 255);
            редактироватьToolStripMenuItem1.ForeColor = Color.FromArgb(42, 45, 129);
            comboBox1.BackColor = Color.FromArgb(183, 212, 255);
            comboBox1.ForeColor = Color.FromArgb(42, 45, 129);
            сохранитьToolStripMenuItem.Visible = false;
            отменитьToolStripMenuItem.Visible = false;
            button2.FlatStyle = FlatStyle.Flat;
            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            int borderRadius2 = 30;
            path2.AddArc(0, 0, borderRadius2, borderRadius2, 180, 90);
            path2.AddArc(button2.Width - borderRadius2, 0, borderRadius2, borderRadius2, 270, 90);
            path2.AddArc(button2.Width - borderRadius2, button2.Height - borderRadius2, borderRadius2, borderRadius2, 0, 90);
            path2.AddArc(0, button2.Height - borderRadius2, borderRadius2, borderRadius2, 90, 90);
            Region region2 = new Region(path2);
            button2.Region = region2;
            button2.BackColor = Color.FromArgb(42, 45, 129);
            button2.ForeColor = Color.FromArgb(183, 212, 255);
            Update_Table();
            string Month = dateTimePicker1.Value.ToString("Y");
            Month = char.ToLower(Month[0]) + Month.Substring(1);
            comboBox1.Text = Month;
            groupBox2.ForeColor= Color.FromArgb(42, 45, 129); 
        }

        public void Load_Items()
        {
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();
                string sql = "SELECT Месяц, Год FROM Рабочий_Период";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string result = "";
                        result += reader["Месяц"].ToString();
                        result += " ";
                        result += reader["Год"].ToString();
                        comboBox1.Items.Add(result);

                    }
                    reader.Close();
                }
                connection.Close();
            }
        }
        int row = 0;
        int procent = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddGraphic();
        }
        private void AddGraphic()
        {
            row = 0;
            Update_Table();
            Graghic.ExcelString.Clear();
            progressBar1.Visible = true;
            dataGridView1.Visible = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn comboBoxColumn = new DataGridViewTextBoxColumn();
            comboBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            comboBoxColumn.HeaderText = "Cотрудники";
            comboBoxColumn.Name = "Column1";
            comboBoxColumn.Width = 100;
            comboBoxColumn.ContextMenuStrip = contextMenuStrip2;
            dataGridView1.Columns.Add(comboBoxColumn);
            Search();
            for (int i = 0; i < Graghic.ExcelString.Count; i++)
            {
                if (SearchName(полный_График_РаботыDataGridView.Rows[Graghic.ExcelString[i]].Cells[0].Value.ToString()) != -1)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].DefaultCellStyle.Font = new Font("PT Sans", 12, FontStyle.Regular);
                    dataGridView1.Rows[row].Cells[0].Value = полный_График_РаботыDataGridView.Rows[Graghic.ExcelString[i]].Cells[0].Value.ToString();
                    dataGridView1.Rows[row].ReadOnly = true;
                    row++;
                }
            }
            AddDays();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                FillingType(i);

            }
            dataGridView1.Visible = true;
            progressBar1.Value = 100;
            progressBar1.Visible = false;
        }
        private void Search()
    {
            string month = "";
            string year = "";
        полный_График_РаботыDataGridView.ClearSelection();
        string search = comboBox1.SelectedItem.ToString();
        for (int i = полный_График_РаботыDataGridView.Rows.Count - 1; i >= 0; i--)
        {
            if (полный_График_РаботыDataGridView.Rows[i].Cells[6].Value != null) { month = полный_График_РаботыDataGridView.Rows[i].Cells[6].Value.ToString(); }
            if (полный_График_РаботыDataGridView.Rows[i].Cells[7].Value != null)
            {
                year = полный_График_РаботыDataGridView.Rows[i].Cells[7].Value.ToString();
            }
            string result = month + " " + year;
            if (search == result)
            {
                Graghic.ExcelString.Add(i);

            }
        }

    }
        private int SearchName(string fam)
        {
            for(int i=0;i<dataGridView1.RowCount;i++)
            {
                if(dataGridView1.Rows[i].Cells[0].Value!=null)
                    if(fam== dataGridView1.Rows[i].Cells[0].Value.ToString())
                { return -1; }
            }
            return dataGridView1.RowCount;
        }
        int days = 0;

        private void AddDays()
        {
            string s = comboBox1.Text;
            string month = "";
            foreach (char c in s)
            {
                if (c != ' ') month += c; else break;
            }
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();

                string sql = "SELECT Количество_Дней FROM Месяц WHERE Месяц = @month";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@fam", month);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        days = int.Parse(result.ToString());
                    }
                }
                connection.Close();
            }
            for(int i=0;i<days;i++)
            {
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.FlatStyle = FlatStyle.Flat;
                comboBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                comboBoxColumn.HeaderText = (i+1).ToString();
                comboBoxColumn.Name = "Column"+ (i + 1).ToString();
                comboBoxColumn.Items.AddRange("Д", "Н", "8", "3");
                comboBoxColumn.Width = 50;
                comboBoxColumn.ContextMenuStrip = contextMenuStrip1;
                // Добавление столбца в DataGridView
                dataGridView1.Columns.Add(comboBoxColumn);
                

            }
        }
        private void FillingType(int n)
        {
            
                for (int j = 0; j < Graghic.ExcelString.Count; j++)
            {
                string type = "";

                for (int i = 0; i < days; i++)
                {

                    if (полный_График_РаботыDataGridView.Rows[Graghic.ExcelString[j]].Cells[0].Value.ToString() == dataGridView1.Rows[n].Cells[0].Value.ToString())
                    {
                        if (полный_График_РаботыDataGridView.Rows[Graghic.ExcelString[j]].Cells[4].Value.ToString() == (i + 1).ToString())
                            if (полный_График_РаботыDataGridView.Rows[Graghic.ExcelString[j]].Cells[5].Value != null) type = полный_График_РаботыDataGridView.Rows[Graghic.ExcelString[j]].Cells[5].Value.ToString();
                    }
                    if (type != "") dataGridView1.Rows[n].Cells[i + 1].Value = type;
                    procent += 100 / (dataGridView1.RowCount * days);
                    progressBar1.Value = procent;
                    type = "";
                }
                
            }
        }

       

        private void добавитьСменуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graghic.Surname = dataGridView1.CurrentCell.Value.ToString();
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void Form4_Activated(object sender, EventArgs e)
        {

        }
        private void Update_Table()
        {
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Полный_График_Работы"; // Замените на свой запрос выборки
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                полный_График_РаботыDataGridView.DataSource = dataTable;
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Подтверите сохранение", "Сохранение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int id = 0;
                int type = 0;
                int sotr = 0;
                int period = 0;
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                if (dataGridView1.Rows[rowIndex].Cells[columnIndex].Value.ToString() == "") { MessageBox.Show("Данные не заполнены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                dataGridView1.Rows[rowIndex].ReadOnly = true;
                //string type = dataGridView1.Rows[rowIndex].Cells[columnIndex].Value.ToString();
                //string sotr = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
                using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
                {
                    connection.Open();

                    //connection.Open();
                    string sql = "SELECT ID_Сотрудник FROM Сотрудник WHERE Фамилия = @fam ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fam", dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
                        object result1 = command.ExecuteScalar();
                        sotr = int.Parse(result1.ToString());
                    }
                    sql = "SELECT ID_Рабочий_Период FROM Рабочий_Период WHERE Месяц = @month AND Год = @year";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        string s = comboBox1.Text;
                        string[] ss = s.Split(' ');
                        string month = ss[0];
                        string year = ss[1];
                        command.Parameters.AddWithValue("@month", month);
                        command.Parameters.AddWithValue("@year", year);
                        object result1 = command.ExecuteScalar();
                        period = int.Parse(result1.ToString());
                    }
                    sql = "SELECT ID_График_Работы FROM График_Работы WHERE ID_Сотрудник = @sotr AND ID_Рабочий_Период = @period AND Число = @chislo ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        int a = int.Parse(dataGridView1.Columns[columnIndex].HeaderText);
                        command.Parameters.AddWithValue("@sotr", sotr);
                        command.Parameters.AddWithValue("@period", period);
                        command.Parameters.AddWithValue("@chislo", a);
                        object result1 = command.ExecuteScalar();
                        id = int.Parse(result1.ToString());
                    }
                    sql = "SELECT ID_Тип_Смены FROM Тип_Смены WHERE Название = @name";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fam", dataGridView1.Rows[rowIndex].Cells[columnIndex].Value.ToString());
                        object result1 = command.ExecuteScalar();
                        type = int.Parse(result1.ToString());
                    }
                     sql = "UPDATE График_Работы SET ID_Тип_Смены = @type WHERE ID_График_Работы = @id";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@type", type);
                        command.Parameters.AddWithValue("@id", id);
                        int a = command.ExecuteNonQuery();
                        try { MessageBox.Show("Данные успешно изменены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при обновлении строки: " + ex.Message);
                        }

                    }
                    connection.Close();
                }
                dataGridView1.CurrentCell.ReadOnly = true;
                сохранитьToolStripMenuItem.Visible = false;
                редактироватьToolStripMenuItem1.Visible = true;
                отменитьToolStripMenuItem.Visible = false;
            }
            else
            {
                return;
            }
        }

        private void редактироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Подтверите изменение", "Изменение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                dataGridView1.CurrentCell.ReadOnly = false;
                сохранитьToolStripMenuItem.Visible = true;
                редактироватьToolStripMenuItem1.Visible = false;
                отменитьToolStripMenuItem.Visible = true;
            }
            else return;

        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.ReadOnly = true;
            сохранитьToolStripMenuItem.Visible = false;
            редактироватьToolStripMenuItem1.Visible = true;
            отменитьToolStripMenuItem.Visible = false;
        }

      
        private void label2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Close();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int l = 1; int j = 1;
            var excelApp = new Excel.Application();

            // Добавляем новую рабочую книгу
            excelApp.Workbooks.Add();

            // Активируем лист
            Excel._Worksheet worksheet = excelApp.ActiveSheet;
            worksheet.Cells[1, 1] = "Сотрудник";
            for(int i=1;i<=days;i++)
            { j++;
              worksheet.Cells[1, j] = i.ToString();
            }
            for(int i=0;i<dataGridView1.RowCount;i++)
            {
                l++;
                worksheet.Cells[l, 1] = dataGridView1.Rows[i].Cells[0].Value;
                for (int k=1;k<=days;k++)
                worksheet.Cells[l, k+1] = dataGridView1.Rows[i].Cells[k].Value;

            }
            string tabl = "A1" + ":AF" + l.ToString();
            Excel.Range range = worksheet.Range[tabl];
            range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium);
            range.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            // Выравнивание текста по центру таблицы
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range.EntireColumn.AutoFit();
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            // Сохраняем рабочую книгу
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls;*.csv";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                excelApp.ActiveWorkbook.SaveAs(saveFileDialog.FileName);
                excelApp.ActiveWorkbook.Saved = true;
            }

            // Закрываем приложение Excel
            excelApp.Quit();

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = 0;
            int sotr = 0;
            int period = 0;
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();
                string sql = "SELECT ID_Сотрудник FROM Сотрудник WHERE Фамилия = @fam ";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@fam", dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
                    object result1 = command.ExecuteScalar();
                    sotr = int.Parse(result1.ToString());
                    Graghic.Surname = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                }
                sql = "SELECT ID_Рабочий_Период FROM Рабочий_Период WHERE Месяц = @month AND Год = @year";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    string s = comboBox1.Text;
                    string[] ss = s.Split(' ');
                    string month = ss[0];
                    string year = ss[1];
                    command.Parameters.AddWithValue("@month", month);
                    command.Parameters.AddWithValue("@year", year);
                    object result1 = command.ExecuteScalar();
                    period = int.Parse(result1.ToString());
                    Graghic.Date += s;
                }
                sql = "SELECT ID_График_Работы FROM График_Работы WHERE ID_Сотрудник = @sotr AND ID_Рабочий_Период = @period AND Число = @chislo ";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    int a = int.Parse(dataGridView1.Columns[columnIndex].HeaderText);
                    command.Parameters.AddWithValue("@sotr", sotr);
                    command.Parameters.AddWithValue("@period", period);
                    command.Parameters.AddWithValue("@chislo", a);
                    Graghic.Date += " " + a.ToString();
                    object result1 = command.ExecuteScalar();
                    id = int.Parse(result1.ToString());
                }
                 sql = "DELETE FROM График_Работы WHERE ID_График_Работы = @id ";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    object result = command.ExecuteScalar();
                    MessageBox.Show("Смена успешно удалена", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Update_Table();
                }
                connection.Close();
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGraphic();

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
