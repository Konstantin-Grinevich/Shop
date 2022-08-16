using Microsoft.Office.Interop.Excel;
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

namespace Shopppppp
{
    public partial class InvoiceForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();

        public InvoiceForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);

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
            workSheet.Cells[1, 1] = $"Накладная по контракту №{id}";
            workSheet.Cells[2, 1] = "Наименование";
            workSheet.Cells[2, 2] = "Количество";
            workSheet.Cells[2, 3] = "Цена";
            workSheet.Cells[2, 4] = "Сумма";

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = $"SELECT p.name, p.quantity, p.price FROM product_in_contract pic JOIN product p ON pic.id_product=p.id WHERE id_contract = {id}";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            con.Close();
            int totalSum = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 1; j < dataTable.Columns.Count + 1; j++)
                {
                    workSheet.Cells[i + 3, j] = dataTable.Rows[i][j-1].ToString();
                }
                int sum = Convert.ToInt32(dataTable.Rows[i][1]) * Convert.ToInt32(dataTable.Rows[i][2]);
                workSheet.Cells[i + 3, 4] = sum;
                totalSum += sum;
            }
            workSheet.Cells[dataTable.Rows.Count + 4, 1] = "Total sum"; 
            workSheet.Cells[dataTable.Rows.Count + 4, 4] = totalSum; 
            workBook.Save();
            InvoiceExcel.Quit();
        }
    }
}
