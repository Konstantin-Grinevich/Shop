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
    public partial class UpdateProductForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        public UpdateProductForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
           
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = $"SELECT name, price, quantity FROM product WHERE id = {id}";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            string name = textBox2.Text;
            if (name.Length == 0)
            {
                name = Convert.ToString(dataTable.Rows[0][0]);
            }
            int price = 0;
            if (textBox3.Text.Length == 0)
                price = Convert.ToInt32(dataTable.Rows[0][1]);
            else price = Convert.ToInt32(textBox3.Text);
            int quantity = 0;
            if (textBox4.Text.Length == 0)
                quantity = Convert.ToInt32(dataTable.Rows[0][2]);
            else quantity = Convert.ToInt32(textBox4.Text);
            NpgsqlCommand command = new NpgsqlCommand("UPDATE product SET name = :name, price = :price, quantity = :quantity WHERE id = :id", con);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("price", price);
            command.Parameters.AddWithValue("quantity", quantity);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }
    }
}
