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
    public partial class SupplierForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        public SupplierForm(System.Data.DataTable dataTable)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new AddSupplierForm();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new UpdateSupplierForm();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new DeleteSupplierForm();
            form.ShowDialog();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = "SELECT * FROM supplier order by id";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            dataGridView1.DataSource = dataTable;
            con.Close();
        }
    }
}
