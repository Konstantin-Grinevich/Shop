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
    public partial class ContractForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        public ContractForm(System.Data.DataTable dataTable)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new AddContractForm();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new UpdateContractForm();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new DeleteContractForm();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new ProductsInContractForm();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form form = new InvoiceForm();
            form.ShowDialog();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new PaymentForm();
            form.ShowDialog();
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
            dataGridView1.DataSource = dataTable;
            con.Close();
        }
    }
}
