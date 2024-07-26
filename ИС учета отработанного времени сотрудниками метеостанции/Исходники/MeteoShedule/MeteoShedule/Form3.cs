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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void сотрудникBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.сотрудникBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.meteoSheduleDataSet);

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "meteoSheduleDataSet.Сотрудник". При необходимости она может быть перемещена или удалена.
            this.сотрудникTableAdapter.Fill(this.meteoSheduleDataSet.Сотрудник);
            сотрудникDataGridView.EnableHeadersVisualStyles = false;
            сотрудникDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(66, 81, 156, 1);
            сотрудникDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            сотрудникDataGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            сотрудникDataGridView.DefaultCellStyle.BackColor = Color.FromArgb(42, 45, 129);
            сотрудникDataGridView.BackgroundColor = Color.FromArgb(42, 45, 129);
            сотрудникDataGridView.DefaultCellStyle.ForeColor = Color.White;
            сотрудникDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            сотрудникDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            сотрудникDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("PT Sans", 12, FontStyle.Bold);
            columnHeaderStyle.BackColor = Color.FromArgb(66, 81, 156);
            сотрудникDataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            label1.BackColor= Color.FromArgb(42, 45, 129);
            contextMenuStrip1.BackColor=Color.FromArgb(183, 211, 255);
            contextMenuStrip1.ForeColor = Color.FromArgb(42, 45, 129);
            уволенныеСотрудникиToolStripMenuItem.BackColor = Color.FromArgb(183, 211, 255);
            уволенныеСотрудникиToolStripMenuItem.ForeColor = Color.FromArgb(42, 45, 129);
            всеСотрудникиToolStripMenuItem.BackColor = Color.FromArgb(183, 211, 255);
            всеСотрудникиToolStripMenuItem.ForeColor = Color.FromArgb(42, 45, 129);
        }

        private void уволенныеСотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search();
        }
        public void Search()
        {
        сотрудникDataGridView.ClearSelection();
        string MeteoSheduleConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MeteoShedule.accdb";
        using (OleDbConnection connection = new OleDbConnection(MeteoSheduleConnection))
            {
             connection.Open();
             string sql = "SELECT Фамилия FROM Уволенные";
             using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                 OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = сотрудникDataGridView.Rows.Count - 1; i >= 0; i--)
                        {
                            if (сотрудникDataGridView.Rows[i].Cells[1].Value != null)
                            {
                                string surname = сотрудникDataGridView.Rows[i].Cells[1].Value.ToString();
                                if (reader["Фамилия"].ToString() == surname)
                                {
                                    int rowIndex = сотрудникDataGridView.Rows[i].Index; // получаем индекс строки
                                    сотрудникDataGridView.Rows[i].Visible = true;
                                    сотрудникDataGridView.CurrentCell = сотрудникDataGridView.Rows[rowIndex].Cells[0]; // выделяем первую ячейку строки

                                }
                                else { сотрудникDataGridView.Rows[i].Visible = false; }
                            } else
                            {
                                int rowIndex = сотрудникDataGridView.Rows[i].Index; // получаем индекс строки
                                сотрудникDataGridView.Rows[i].Visible = true;
                                сотрудникDataGridView.CurrentCell = сотрудникDataGridView.Rows[rowIndex].Cells[0]; // выделяем первую ячейку строки
                            }

                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
        }

        private void всеСотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = сотрудникDataGridView.Rows.Count - 1; i >= 0; i--)
            {
                int rowIndex = сотрудникDataGridView.Rows[i].Index; // получаем индекс строки
                сотрудникDataGridView.Rows[i].Visible = true;
                сотрудникDataGridView.CurrentCell = сотрудникDataGridView.Rows[rowIndex].Cells[0]; // выделяем первую ячейку строки

            }
        }

        private void фильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выбратьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            сотрудникDataGridView.CurrentCell = сотрудникDataGridView.Rows[0].Cells[0];
            GlobalList.Surnames.Clear();
            for (int i = сотрудникDataGridView.Rows.Count - 2; i >= 0; i--)
            {
                if((сотрудникDataGridView.Rows[i].Cells[0].Value!=null))
                if ((bool)сотрудникDataGridView.Rows[i].Cells[0].Value==true)
                {
                    GlobalList.Surnames.Add(сотрудникDataGridView.Rows[i].Cells[1].Value.ToString());
                    GlobalList.index = 1;
                }

            }
            MessageBox.Show("Фильтр успешно сохранен", "Фильтр", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            
        }
    }
}
