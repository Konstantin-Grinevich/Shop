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
    public partial class UpdateContractForm : Form
    {
        public UpdateContractForm()
        {
            InitializeComponent();
        }

        private void UpdateContractForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            int idSup = Convert.ToInt32(textBox2.Text);
            DateTime date = dateTimePicker1.Value;

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("UPDATE contract SET id_sup = :idSup, date = :date WHERE id = :id", con);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("idSup", idSup);
            command.Parameters.AddWithValue("date", date);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
