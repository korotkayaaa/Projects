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

namespace MeteoShedule
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(183, 212, 255);
            textBox1.ForeColor = Color.FromArgb(42, 45, 129);
            comboBox1.BackColor = Color.FromArgb(183, 212, 255);
            comboBox1.ForeColor = Color.FromArgb(42, 45, 129);
            button2.FlatStyle = FlatStyle.Flat;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int borderRadius = 30;
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(button2.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(button2.Width - borderRadius, button2.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, button2.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            Region region = new Region(path);
            button2.Region = region;
            button2.BackColor = Color.FromArgb(42, 45, 129);
            button2.ForeColor = Color.FromArgb(183, 212, 255);
            textBox1.Text = Graghic.Surname;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(monthCalendar1.SelectionEnd!=null && comboBox1.SelectedIndex!=-1)
            {
                int id = 0;
                int sotr = 0;
                int type = 0;
                int peroid = 0;
                string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
                using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
                {
                    connection.Open();

                    string sql = "SELECT ID_График_Работы FROM График_Работы";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        OleDbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if(id< int.Parse(reader["ID_График_Работы"].ToString()))id = int.Parse(reader["ID_График_Работы"].ToString());
                        }
                        reader.Close();
                        id++;
                    }
                     sql = "SELECT ID_Сотрудник FROM Сотрудник WHERE Фамилия = @fam ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fam", textBox1.Text);
                        object result = command.ExecuteScalar();
                        sotr = int.Parse(result.ToString());
                    }
                    sql = "SELECT ID_Тип_Смены FROM Тип_Смены WHERE Название = @name ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", comboBox1.Text);
                        object result = command.ExecuteScalar();
                        type = int.Parse(result.ToString());
                    }
                    sql = "SELECT ID_Рабочий_Период FROM Рабочий_Период WHERE Месяц = @month AND Год = @year ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        string Month = monthCalendar1.SelectionEnd.ToString("MMMM");
                        Month = char.ToLower(Month[0]) + Month.Substring(1);
                        command.Parameters.AddWithValue("@month", Month);
                        command.Parameters.AddWithValue("@year", monthCalendar1.SelectionEnd.ToString("yyyy"));
                        object result = command.ExecuteScalar();
                        peroid = int.Parse(result.ToString());
                    }
                    sql = "INSERT INTO График_Работы ([ID_График_Работы], [ID_Тип_Смены], [ID_Сотрудник], [ID_Рабочий_Период], [Число]) VALUES (@id, @id2, @id3, @id4, @chislo) ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id+1);
                        command.Parameters.AddWithValue("@id2", type);
                        command.Parameters.AddWithValue("@id3", sotr);
                        command.Parameters.AddWithValue("@id4", peroid);
                        command.Parameters.AddWithValue("@chislo", monthCalendar1.SelectionEnd.ToString("dd"));
                        command.ExecuteNonQuery();

                    }
                    sql = "INSERT INTO Неявки (ID_График_Работы) VALUES (@id)";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id + 1);
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                    MessageBox.Show("Смена успешно добавлена", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

       
    
}
