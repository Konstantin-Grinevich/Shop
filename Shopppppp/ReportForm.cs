using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Collections.ObjectModel;

namespace Shopppppp
{
    public partial class ReportForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        public ReportForm()
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT name FROM supplier order by id";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            con.Close();
            for (int i =0; i < dataTable.Rows.Count; i++)
                checkedListBox1.Items.Add(dataTable.Rows[i][0]);
                
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();

            string sups = $"('{checkedListBox1.CheckedItems[0]}'";
            for (int i = 1; i < checkedListBox1.CheckedItems.Count; i++)
            {
                string s = Convert.ToString(checkedListBox1.CheckedItems[i]);
                sups += $", '{s}'";
            }
            sups += ")";

            string sql = $"SELECT SUM(total_sum), SUM(paid_sum) FROM contract WHERE id_sup IN (SELECT id FROM supplier WHERE name IN {sups}) AND (date BETWEEN '{startDate}' AND '{endDate}')";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            con.Close();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string fileName = ofd.FileName;
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            if (word == null)
            {
                MessageBox.Show("Beda");
                return;
            }

            word.Visible = true;
            word.WindowState = WdWindowState.wdWindowStateNormal;
            Microsoft.Office.Interop.Word.Document doc = word.Documents.Add();
            Microsoft.Office.Interop.Word.Paragraph paragraph = doc.Paragraphs.Add();
            paragraph.Range.Text = $"Для поставщиков {sups}; за период времени с {startDate} по {endDate}\n" +
                $"Оборот составляет: {dataTable.Rows[0][0]}\n" +
                $"Неоплаченная сумма: {dataTable.Rows[0][1]} ";
            try
            {
                doc.SaveAs2("kyky.docx");
            } catch(System.Runtime.InteropServices.COMException ex)
            {

            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
