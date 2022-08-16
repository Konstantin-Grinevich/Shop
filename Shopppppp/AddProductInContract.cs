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
    public partial class AddProductInContract : Form
    {
        public AddProductInContract()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idContract = Convert.ToInt32(textBox1.Text);
            int idProduct = Convert.ToInt32(textBox2.Text);

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO product_in_contract (id_contract, id_product) VALUES (:idContract, :idProduct)", con);
            command.Parameters.AddWithValue("idContract", idContract);
            command.Parameters.AddWithValue("idProduct", idProduct);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }
    }
}
