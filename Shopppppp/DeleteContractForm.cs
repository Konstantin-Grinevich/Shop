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
    public partial class DeleteContractForm : Form
    {
        public DeleteContractForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM contract WHERE id = :id", con);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }

        private void DeleteContractForm_Load(object sender, EventArgs e)
        {

        }
    }
}
