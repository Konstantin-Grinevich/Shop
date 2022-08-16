using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Npgsql;

namespace Shopppppp
{
    public partial class MainForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string fileName = ofd.FileName;
            Microsoft.Office.Interop.Excel.Application InvoiceExcel = new Microsoft.Office.Interop.Excel.Application();
            if (InvoiceExcel == null)
            {
                MessageBox.Show("Beda");
                return;
            }
            Workbook workBook = InvoiceExcel.Workbooks.Open(fileName, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false);
            Worksheet workSheet = workBook.Sheets[1];
            workSheet.Cells[1, 1] = "Наименование";
            workSheet.Cells[1, 2] = "Количество";
            workSheet.Cells[1, 3] = "Цена";
            workSheet.Cells[1, 4] = "Сумма";

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM product";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            con.Close();

            for (int i = 0; i < dataTable.Rows.Count; i++)
                for(int j = 1; j < dataTable.Columns.Count; j++)
                {
                    workSheet.Cells[i + 2, j] = dataTable.Rows[i][j].ToString();
                }

            workBook.Save();
            InvoiceExcel.Quit();


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM product";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            Form form = new ProductForm(dataTable);
            form.ShowDialog();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM supplier";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            Form form = new ProductForm(dataTable);
            form.ShowDialog();
            con.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM product order by id";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            Form form = new ProductForm(dataTable);
            form.ShowDialog();
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM supplier order by id";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            Form form = new SupplierForm(dataTable);
            form.ShowDialog();
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM contract_status order by id";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            Form form = new ContractForm(dataTable);
            form.ShowDialog();
            con.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form form = new ReportForm();
            form.ShowDialog();
        }
    }
}
