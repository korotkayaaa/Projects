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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int borderRadius = 40;
            path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            path.AddArc(button1.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            path.AddArc(button1.Width - borderRadius, button1.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            path.AddArc(0, button1.Height - borderRadius, borderRadius, borderRadius, 90, 90);
            Region region = new Region(path);
            button1.Region = region;
            button1.BackColor = Color.FromArgb(42, 45, 129);
            button1.ForeColor = Color.FromArgb(183, 212, 255);
            label2.BackColor = Color.FromArgb(42, 45, 129);
            textBox1.BackColor = Color.FromArgb(183, 212, 255);
            textBox1.ForeColor = Color.FromArgb(42, 45, 129);
            textBox2.BackColor = Color.FromArgb(183, 212, 255);
            textBox2.ForeColor = Color.FromArgb(42, 45, 129);
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Login") textBox1.Text = "";
        }


        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Password") textBox2.Text = "";

        }


        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { textBox1.Text = "Login"; };

        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            if (textBox2.Text == "") { textBox2.Text = "Password"; };
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int entry = 0;
            string login="";
            string surname = "";

            if (CorrectData(textBox1) && CorrectData(textBox2))
            {
                string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
                using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
                {
                    connection.Open();

                    string sql = "SELECT Логин FROM Сотрудник";
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            login = Convert.ToString(result);
                            if (login == textBox1.Text) entry += 1;
                        }
                    }
                    string sq2 = "SELECT Пароль FROM Сотрудник";
                    using (OleDbCommand command = new OleDbCommand(sq2, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            string password = Convert.ToString(result);
                            if (password == textBox2.Text) entry += 1;
                        }
                    }
                    connection.Close();
                }
                if(entry==2)
                {
                    using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
                    {
                        connection.Open();

                        string query = "SELECT Имя FROM Сотрудник WHERE Логин = @login";
                        OleDbCommand command = new OleDbCommand(query, connection);
                        command.Parameters.AddWithValue("@login", login);
                        object result = command.ExecuteScalar();
                        surname = Convert.ToString(result);
                        connection.Close();
                    }
                    MessageBox.Show(surname+ ", вы успешно вошли в систему!", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.Show();
                } else
                {
                    MessageBox.Show("Данные введены неверно", "Вход", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "Login";
                    textBox2.Text = "Password";
                }
            }
        }
        public static bool CorrectData(TextBox a)
        {
            if(a.Text=="" || a.Text=="Login" || a.Text=="Password")
            {
                MessageBox.Show("Введите все данные для входа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (a.Text.Length<8)
            {
                MessageBox.Show("Данные введены не полностью", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           

        }
    }
}
