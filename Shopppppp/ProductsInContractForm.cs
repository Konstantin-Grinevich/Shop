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
    public partial class ProductsInContractForm : Form
    {

        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        public ProductsInContractForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = $"SELECT pic.id, pic.id_contract, p.name, p.price, p.quantity FROM product_in_contract pic JOIN product p ON pic.id_product=p.id WHERE id_contract = {id}";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            dataGridView1.DataSource = dataTable;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ProductsInContractForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new AddProductInContract();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new UpdateProductInContract();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new DeleteProductInContract();
            form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
