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
    public partial class UpdateSupplierForm : Form
    {
        public UpdateSupplierForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string name = textBox2.Text;

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE supplier SET name = :name WHERE id = :id", con);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", name);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }
    }
}
