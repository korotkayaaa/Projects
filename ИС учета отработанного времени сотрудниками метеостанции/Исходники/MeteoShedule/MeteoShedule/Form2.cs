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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
namespace MeteoShedule
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            // TODO: данная строка кода позволяет загрузить данные в таблицу "meteoSheduleDataSet.Табель_Учета". При необходимости она может быть перемещена или удалена.
            this.табель_УчетаTableAdapter.Fill(this.meteoSheduleDataSet.Табель_Учета);
            табель_УчетаDataGridView.EnableHeadersVisualStyles = false;
            табель_УчетаDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(66, 81, 156);
            табель_УчетаDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            табель_УчетаDataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            табель_УчетаDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(42, 45, 129);
            табель_УчетаDataGridView.BackgroundColor = Color.FromArgb(42, 45, 129);
            табель_УчетаDataGridView.DefaultCellStyle.ForeColor = Color.White;
            табель_УчетаDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            табель_УчетаDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            табель_УчетаDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("PT Sans", 12, FontStyle.Bold);
            columnHeaderStyle.BackColor = Color.FromArgb(66, 81, 156);
            табель_УчетаDataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            groupBox1.ForeColor = Color.FromArgb(42, 45, 129);
            comboBox1.BackColor=Color.FromArgb(183, 212, 255);
            comboBox1.ForeColor = Color.FromArgb(42, 45, 129);
            comboBox2.BackColor = Color.FromArgb(183, 212, 255);
            checkBox1.ForeColor = Color.FromArgb(42, 45, 129);
            label4.ForeColor = Color.FromArgb(42, 45, 129);
            label5.ForeColor = Color.FromArgb(42, 45, 129);
            label6.ForeColor = Color.FromArgb(42, 45, 129);       
            CustomizeDateTimePicker(dateTimePicker1);
            comboBox1.Items.Clear();
            Load_Items();
            button1.FlatStyle = FlatStyle.Flat;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int borderRadius = 30;
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(button1.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(button1.Width - borderRadius, button1.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, button1.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            Region region = new Region(path);
            button1.Region = region;
            button1.BackColor = Color.FromArgb(42, 45, 129);
            groupBox2.ForeColor = Color.FromArgb(42, 45, 129);
            button1.ForeColor = Color.FromArgb(183, 212, 255);
            for (int i=0; i< табель_УчетаDataGridView.Rows.Count; i++)
            {
                табель_УчетаDataGridView.Rows[i].ReadOnly = true;
            }
            табель_УчетаDataGridView.Columns[5].ReadOnly = false;
            for (int i = 0; i < табель_УчетаDataGridView.Rows.Count; i++)
            {
                табель_УчетаDataGridView.Rows[i].Cells[5].Value = "Изменить";
                   
            }
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
            contextMenuStrip1.BackColor = Color.FromArgb(183, 211, 255);
            contextMenuStrip1.ForeColor = Color.FromArgb(42, 45, 129);
            Update_Table();
            Search_Data();
        }
        public void Load_Items()
        {        
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();
                string sql = "SELECT Месяц, Год FROM Рабочий_Период" ;
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string result = "";
                        result+= reader["Месяц"].ToString();
                        result += " ";
                        result += reader["Год"].ToString();
                        comboBox1.Items.Add(result);
                        comboBox2.Items.Add(result);

                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                checkBox1.Checked = false;
                Search();
            }

        }

        public  void Search()
        {
            табель_УчетаDataGridView.ClearSelection();
            string search = comboBox1.SelectedItem.ToString();
            for (int i = табель_УчетаDataGridView.Rows.Count - 1; i >= 0; i--)
            {
                string month = ""; string year = "";
                if (табель_УчетаDataGridView.Rows[i].Cells["Месяц"].Value != null) { month = табель_УчетаDataGridView.Rows[i].Cells["Месяц"].Value.ToString(); }
                if (табель_УчетаDataGridView.Rows[i].Cells["Год"].Value != null)
                {
                    year = табель_УчетаDataGridView.Rows[i].Cells["Год"].Value.ToString();
                }
                string result = month + " " + year;
                if (search == result || result == " ")
                {
                    int rowIndex = табель_УчетаDataGridView.Rows[i].Index; // получаем индекс строки
                    табель_УчетаDataGridView.Rows[i].Visible = true;
                    табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[rowIndex].Cells[0]; // выделяем первую ячейку строки

                }
                else { табель_УчетаDataGridView.Rows[i].Visible = false; }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                comboBox1.Text = "";
                for (int i = табель_УчетаDataGridView.Rows.Count - 1; i >= 0; i--)
                {
                    табель_УчетаDataGridView.Rows[i].Visible = true;

                }
                GlobalList.index = 0;
                GlobalList.Surnames.Clear();
                checkBox1.Checked=false;
                Search_Data();
            }
        }
        private void CustomizeDateTimePicker(DateTimePicker dateTimePicker)
        {
            // Устанавливаем синий цвет для фона DateTimePicker
            dateTimePicker.BackColor = Color.FromArgb(42, 45, 129);
            // Устанавливаем белый цвет для текста DateTimePicker
            dateTimePicker.ForeColor = Color.White;
            // Устанавливаем пользовательский формат даты и времени
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd.MM.yyyy";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            checkBox1.Checked = false;
            Search_Data();
            
        }
        public void Search_Data()
        {
            табель_УчетаDataGridView.ClearSelection();
            string day = dateTimePicker1.Value.Day.ToString();
            string month = dateTimePicker1.Value.ToString("MMMM");
            string year= dateTimePicker1.Value.Year.ToString();
            string search = day + " " + month + " " + year;
            for (int i = табель_УчетаDataGridView.Rows.Count - 1; i >= 0; i--)
            {
                string Month = ""; string Year = ""; string Day = "";
                if (табель_УчетаDataGridView.Rows[i].Cells["Месяц"].Value != null) {
                    Month = табель_УчетаDataGridView.Rows[i].Cells["Месяц"].Value.ToString();
                   if(Month!="") Month = char.ToUpper(Month[0]) + Month.Substring(1);
                }
                if (табель_УчетаDataGridView.Rows[i].Cells[1].Value != null)
                {
                    Day = табель_УчетаDataGridView.Rows[i].Cells[1].Value.ToString();
                }
                if (табель_УчетаDataGridView.Rows[i].Cells["Месяц"].Value != null)
                {
                    Year = табель_УчетаDataGridView.Rows[i].Cells["Год"].Value.ToString();
                }
                string result = Day+ " "+ Month + " " + Year;
                if (search == result || result == "  ")
                {
                    int rowIndex = табель_УчетаDataGridView.Rows[i].Index; // получаем индекс строки
                    табель_УчетаDataGridView.Rows[i].Visible = true;
                    табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[rowIndex].Cells[0]; // выделяем первую ячейку строки

                }
                else { табель_УчетаDataGridView.Rows[i].Visible = false; }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form3 f3 = new Form3();
            f3.ShowDialog();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                if (GlobalList.index == 1)
                {
                    foreach (DataGridViewRow row in табель_УчетаDataGridView.Rows)
                    {
                        string surname = null;
                        if (row.Cells[0].Value != null) surname = row.Cells[0].Value.ToString();

                        if (GlobalList.Surnames.Contains(surname) || surname == null || surname == "")
                        {
                            int rowIndex = row.Index;
                            row.Visible = true;
                            табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[rowIndex].Cells[0];
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                }
                else { MessageBox.Show("Выберите сотрудников для фильтра", "Фильтр", MessageBoxButtons.OK, MessageBoxIcon.Error); checkBox2.Checked = false; }

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                if (comboBox1.Text != "")
                {
                    if (GlobalList.index == 1)
                    {
                        табель_УчетаDataGridView.ClearSelection();
                        string search = comboBox1.SelectedItem.ToString();
                        foreach (DataGridViewRow row in табель_УчетаDataGridView.Rows)
                        {
                            string surname = null;
                            if (row.Cells[0].Value != null) surname = row.Cells[0].Value.ToString();
                            string month = ""; string year = "";
                            if (row.Cells["Месяц"].Value != null) { month = row.Cells["Месяц"].Value.ToString(); }
                            if (row.Cells["Месяц"].Value != null)
                            {
                                year = row.Cells["Год"].Value.ToString();
                            }
                            string result = month + " " + year;
                            if ((GlobalList.Surnames.Contains(surname) && search == result) || ((surname == null || surname == "") && (result == null || result == " ")))
                            {
                                int rowIndex = row.Index;
                                row.Visible = true;
                                табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[rowIndex].Cells[0];
                            }
                            else
                            {
                                row.Visible = false;
                            }
                        }
                    }
                    else MessageBox.Show("Выберите сотрудников для фильтра", "Фильтр", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (GlobalList.index == 1)
                    {
                        табель_УчетаDataGridView.ClearSelection();
                        string day = dateTimePicker1.Value.Day.ToString();
                        string month = dateTimePicker1.Value.ToString("MMMM");
                        string year = dateTimePicker1.Value.Year.ToString();
                        string search = day + " " + month + " " + year;
                        foreach (DataGridViewRow row in табель_УчетаDataGridView.Rows)
                        {
                            string surname = null;
                            if (row.Cells[0].Value != null) surname = row.Cells[0].Value.ToString();
                            string Month = ""; string Year = ""; string Day = "";
                            if (row.Cells["Месяц"].Value != null)
                            {
                                Month = row.Cells["Месяц"].Value.ToString();
                                Month = char.ToUpper(Month[0]) + Month.Substring(1);
                            }
                            if (row.Cells[1].Value != null)
                            {
                                Day = row.Cells[1].Value.ToString();
                            }
                            if (row.Cells["Месяц"].Value != null)
                            {
                                Year = row.Cells["Год"].Value.ToString();
                            }
                            string result = Day + " " + Month + " " + Year;
                            if ((GlobalList.Surnames.Contains(surname) && search == result) || ((surname == null || surname == "") && (result == null || result == "  ")))
                            {
                                int rowIndex = row.Index;
                                row.Visible = true;
                                табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[rowIndex].Cells[0];
                            }
                            else
                            {
                                row.Visible = false;
                            }
                        }
                    }
                    else MessageBox.Show("Выберите сотрудников для фильтра", "Фильтр", MessageBoxButtons.OK, MessageBoxIcon.Error); checkBox3.Checked = false;
                }
            }
        }

        private void табель_УчетаDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                var buttonCell = (DataGridViewButtonCell)табель_УчетаDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (buttonCell.Value.ToString() == "Изменить")
                {
                    DialogResult result = MessageBox.Show("Подтверите изменение", "Изменение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(result == DialogResult.OK)
                    {
                        табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[e.RowIndex].Cells[0];
                        табель_УчетаDataGridView.Rows[e.RowIndex].ReadOnly = false;
                        buttonCell.Value = "Сохранить";
                        табель_УчетаDataGridView.Columns[5].ReadOnly = false;
                    } else
                    {
                        return;
                    }
             

                }
                else
                {
                    
                    DialogResult result = MessageBox.Show("Подтверите сохранение", "Сохранение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        if (CorrectData(e.RowIndex) == false) { MessageBox.Show("Не все данные заполнены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        if((IsValue(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), 0))==false || IsValue(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), 1)== false || IsValue(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), 2)== false)
                                {
                            return;
                        }
                        табель_УчетаDataGridView.CurrentCell = табель_УчетаDataGridView.Rows[табель_УчетаDataGridView.Rows.Count - 1].Cells[0];
                        табель_УчетаDataGridView.Rows[e.RowIndex].ReadOnly = true;
                        табель_УчетаDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Изменить";
                        табель_УчетаDataGridView.Columns[5].ReadOnly = false;
                        string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
                        string sql = "";
                        using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
                        {
                            connection.Open();
                            if (табель_УчетаDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() =="")
                            {

                                 sql = "UPDATE Табель_Учета SET Фамилия = @fam, Число = @chislo, [Тип_Смены].Название = @type WHERE ID_График_Работы = @id";
                                using (OleDbCommand command = new OleDbCommand(sql, connection))
                                {
                                    command.Parameters.AddWithValue("@fam", табель_УчетаDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                                    command.Parameters.AddWithValue("@chislo", int.Parse(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()));
                                    command.Parameters.AddWithValue("@type", табель_УчетаDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                                    command.Parameters.AddWithValue("@id", int.Parse(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()));
                                    int a = command.ExecuteNonQuery();
                                    try { MessageBox.Show("Данные успешно изменены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Ошибка при обновлении строки: " + ex.Message);
                                    }
                                }
                            } else
                            {
                                 sql = "UPDATE Табель_Учета SET Фамилия = @fam, Имя = @im, Отчество = @otch, Число = @chislo, [Тип_Смены].Название = @type, Количество_Часов = @chas, Месяц = @month, Год = @year, [Должность].Название = @dol WHERE ID_График_Работы = @id";
                                using (OleDbCommand command = new OleDbCommand(sql, connection))
                                {
                                    command.Parameters.AddWithValue("@fam", табель_УчетаDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                                    command.Parameters.AddWithValue("@im", табель_УчетаDataGridView.Rows[e.RowIndex].Cells["Имя"].Value.ToString());
                                    command.Parameters.AddWithValue("@otch", табель_УчетаDataGridView.Rows[e.RowIndex].Cells["Отчество"].Value.ToString());
                                    command.Parameters.AddWithValue("@chislo", int.Parse(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()));
                                    command.Parameters.AddWithValue("@type", табель_УчетаDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                                    command.Parameters.AddWithValue("@chas", int.Parse(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()));
                                    command.Parameters.AddWithValue("@month", табель_УчетаDataGridView.Rows[e.RowIndex].Cells["Месяц"].Value.ToString());
                                    command.Parameters.AddWithValue("@year", табель_УчетаDataGridView.Rows[e.RowIndex].Cells["Год"].Value.ToString());
                                    command.Parameters.AddWithValue("@dol", табель_УчетаDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                                    command.Parameters.AddWithValue("@id", int.Parse(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()));
                                    int a = command.ExecuteNonQuery();
                                    try { MessageBox.Show("Данные успешно изменены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Ошибка при обновлении строки: " + ex.Message);
                                    }
                                }
                            }
                            if (табель_УчетаDataGridView.Rows[e.RowIndex].Cells[3].Value!=null)
                            {
                                sql = "UPDATE Виды_Неявки SET Название = @vid WHERE ID_График_Работы = @id";
                                using (OleDbCommand command = new OleDbCommand(sql, connection))
                                {
                                    command.Parameters.AddWithValue("@vid", табель_УчетаDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    command.Parameters.AddWithValue("@id", int.Parse(табель_УчетаDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()));
                                    int a = command.ExecuteNonQuery();
                                    try { }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Ошибка при обновлении строки: " + ex.Message);
                                    }
                                }
                            }
                                    connection.Close();
                                    табель_УчетаDataGridView.Update();
                                }
                            }
                    else
                    {
                        return;
                    }
                }
            }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Период не выбран", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                // Создаем экземпляр приложения Excel
                var excelApp = new Excel.Application();

            // Добавляем новую рабочую книгу
            excelApp.Workbooks.Add();

            // Активируем лист
            Excel._Worksheet worksheet = excelApp.ActiveSheet;

            string search = comboBox2.SelectedItem.ToString();
            for (int i = табель_УчетаDataGridView.Rows.Count - 2; i >= 0; i--)
            {
                string month = ""; string year = "";
                month = табель_УчетаDataGridView.Rows[i].Cells["Месяц"].Value.ToString();                
                year = табель_УчетаDataGridView.Rows[i].Cells["Год"].Value.ToString();
                
                string result = month + " " + year;
                if (search == result )
                {
                    GlobalList.ExcelString.Add(табель_УчетаDataGridView.Rows[i].Index);

                }
              
            }
            worksheet.Cells[1, 1] = "ФИО";
            int l = 1;
            int j = 1;
            foreach(int stroka in GlobalList.ExcelString)
            {
                string result = табель_УчетаDataGridView.Rows[stroka].Cells[0].Value + " ";
                string name = табель_УчетаDataGridView.Rows[stroka].Cells["Имя"].Value.ToString();
                result += name[0];
                result += ". ";
                string surname = табель_УчетаDataGridView.Rows[stroka].Cells["Отчество"].Value.ToString();
                result += surname[0];
                result += ".";
                l++;
                worksheet.Cells[l, 1] = result;
             }
            l = 1;
            j++;
            worksheet.Cells[1, j] = "Должность";
            foreach (int stroka in GlobalList.ExcelString)
            {
                string result = табель_УчетаDataGridView.Rows[stroka].Cells[11].Value + " ";             
                l++;
                worksheet.Cells[l, j] = result;
            }
            l = 1;
            int days = 0;
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();

                string sql = "SELECT Количество_Дней FROM Месяц WHERE Месяц = @month";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    string month = "";
                    string res = comboBox2.Text;
                    foreach (char c in res)
                    {

                        if (c != ' ')
                        {
                            month += c;
                        }
                        else break;
                    }
                    command.Parameters.AddWithValue("@month", month);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        days = int.Parse(result.ToString());
                    }
                    connection.Close();
                }
            }
            j++;
            for(int i=1;i<=days;i++)
            {
                worksheet.Cells[l, j] = i;
                foreach (int stroka in GlobalList.ExcelString)
                {
                    if (табель_УчетаDataGridView.Rows[stroka].Cells[1].Value.ToString() == i.ToString()) { l++;  worksheet.Cells[l, j] = табель_УчетаDataGridView.Rows[stroka].Cells[3].Value; } else { l++; }
                    
                }
                l = 1;
                j++;
            }
            worksheet.Cells[l, j] = "Кол-во факт.отраб. часов";//дописать
            j++;
            worksheet.Cells[l, j] = "Кол-во часов к оплате";//дописать
            j++;
            string tabl = "A" + l.ToString() + ":AI10";
            Excel.Range range = worksheet.Range[tabl] ;
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
    private bool CorrectData(int i)
        {
            if(табель_УчетаDataGridView.Rows[i].Cells[0].Value.ToString() != "")
            {
                if (табель_УчетаDataGridView.Rows[i].Cells[1].Value.ToString() != "")
                {
                    if (табель_УчетаDataGridView.Rows[i].Cells[2].Value.ToString() != "")
                    {
                            return true;
                        }
                    else { return false; }
                }
                else { return false; }
            } else { return false; }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            }

        private void label8_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultt = MessageBox.Show("Подтвердите удаление", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultt != DialogResult.OK) return;
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();
                string sql = "DELETE FROM Неявки WHERE ID_График_Работы = @id ";
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    int rowIndex = табель_УчетаDataGridView.CurrentCell.RowIndex;
                    command.Parameters.AddWithValue("@id", int.Parse(табель_УчетаDataGridView.Rows[rowIndex].Cells[10].Value.ToString()));
                    object result = command.ExecuteScalar();
                    MessageBox.Show("Неявка успешно удалена", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Update_Table();
                }
                connection.Close();
            }
        }
        private void Update_Table()
        {
            string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
            using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Табель_Учета"; // Замените на свой запрос выборки
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                табель_УчетаDataGridView.DataSource = dataTable;
                for (int i = 0; i < табель_УчетаDataGridView.Rows.Count; i++)
                {
                    табель_УчетаDataGridView.Rows[i].ReadOnly = true;
                }
                табель_УчетаDataGridView.Columns[5].ReadOnly = false;
                for (int i = 0; i < табель_УчетаDataGridView.Rows.Count; i++)
                {
                    табель_УчетаDataGridView.Rows[i].Cells[5].Value = "Изменить";

                }
                for (int i = 0; i < табель_УчетаDataGridView.Rows.Count - 1; i++)
                {
                    string sql = "SELECT Название FROM Виды_Неявки WHERE ID_График_Работы = @id ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", int.Parse(табель_УчетаDataGridView.Rows[i].Cells[10].Value.ToString()));
                        object result = command.ExecuteScalar();
                        if(result!=null)табель_УчетаDataGridView.Rows[i].Cells[3].Value = result.ToString();
                    }
                }
            }
        }

        private void табель_УчетаDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           

        }
        private bool IsValue(string value, int i)
        {
            if(i==0)
            {
                foreach (char c in value)
                {
                    if ((c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я'))
                    {
                       
                    } else
                    {
                        MessageBox.Show("Данные введены некорректно", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                } return true;
            }
            if(i==1)
            {
                foreach (char c in value)
                {
                    if ((c >= '0' && c <= '9'))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Данные введены некорректно", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            if (i == 2)
            {
                foreach (char c in value)
                {
                    if ((c >= '0' && c <= '4'))
                    {

                    }
                    else
                    {
                        MessageBox.Show("Данные введены некорректно", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            return true;

        }

        private void табель_УчетаDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void зарегистрироватьНеявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graghic.Surname = табель_УчетаDataGridView.Rows[табель_УчетаDataGridView.CurrentCell.RowIndex].Cells[0].Value.ToString();
            Graghic.Date= табель_УчетаDataGridView.Rows[табель_УчетаDataGridView.CurrentCell.RowIndex].Cells[1].Value.ToString() +" " + comboBox1.Text;
            Graghic.index= int.Parse(табель_УчетаDataGridView.Rows[табель_УчетаDataGridView.CurrentCell.RowIndex].Cells[10].Value.ToString());
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void Form2_Activated(object sender, EventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update_Table();
            comboBox1_TextChanged(sender, e);

        }
    }
}
