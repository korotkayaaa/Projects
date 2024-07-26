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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(183, 212, 255);
            textBox1.ForeColor = Color.FromArgb(42, 45, 129);
            textBox2.BackColor = Color.FromArgb(183, 212, 255);
            textBox2.ForeColor = Color.FromArgb(42, 45, 129); 
            textBox3.BackColor = Color.FromArgb(183, 212, 255);
            textBox3.ForeColor = Color.FromArgb(42, 45, 129);
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
            textBox2.Text = Graghic.Date;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && textBox3.Text != "")
            {
                int type = 0;
                string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
                using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
                {
                    connection.Open();
                    string sql = "SELECT ID_Вид_Неявки FROM Вид_Неявки WHERE Название = @name ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", comboBox1.Text);
                        object result = command.ExecuteScalar();
                        type = int.Parse(result.ToString());
                    }
                    sql = "UPDATE Неявки SET [ID_Вид_Неявки] = @id2, [Количество_Часов] = @chislo WHERE ID_График_Работы = @id ";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id2", type);
                        command.Parameters.AddWithValue("@chislo", textBox3.Text);
                        command.Parameters.AddWithValue("@id", Graghic.index);
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                    MessageBox.Show("Неявка успешно зарегистрирована", "Регистрация неявки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
