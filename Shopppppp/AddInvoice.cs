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
    public partial class AddInvoice : Form
    {
        public AddInvoice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idSup = Convert.ToInt32(textBox1.Text);
            int totalSum = Convert.ToInt32(textBox2.Text);
            int paidSum = Convert.ToInt32(textBox3.Text);
            DateTime date = dateTimePicker1.Value;
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO invoice (id_contract, total_sum, paid_sum, date) VALUES (:idSup, :totalSum, :paidSum, :date)", con);
            command.Parameters.AddWithValue("idSup", idSup);
            command.Parameters.AddWithValue("totalSum", totalSum);
            command.Parameters.AddWithValue("paidSum", paidSum);
            command.Parameters.AddWithValue("date", date);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }
    }
}
