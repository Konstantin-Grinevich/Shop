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
    public partial class UpdateProductInContract : Form
    {
        public UpdateProductInContract()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            int idSup = Convert.ToInt32(textBox2.Text);
            int totalSum = Convert.ToInt32(textBox2.Text);

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE product_in_contract SET id_contract = :idSup, id_product = :totalSum WHERE id = :id", con);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("idSup", idSup);
            command.Parameters.AddWithValue("totalSum", totalSum);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
