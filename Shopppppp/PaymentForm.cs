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
    public partial class PaymentForm : Form
    {
        private DataSet dataSet = new DataSet();
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        public PaymentForm()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            string sql = $"SELECT total_sum, paid_sum FROM contract WHERE id = {id}";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);
            dataSet.Reset();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            string dif = Convert.ToString(Convert.ToInt32(dataTable.Rows[0][0]) - Convert.ToInt32(dataTable.Rows[0][1]));
            label2.Text = "Можно внести не более: " + dif;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            int sum = Convert.ToInt32(textBox2.Text);

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE contract SET paid_sum = paid_sum + :sum WHERE id = :id", con);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("sum", sum);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
