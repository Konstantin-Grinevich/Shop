﻿using Npgsql;
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
    public partial class AddSupplierForm : Form
    {
        public AddSupplierForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;

            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; UserId = postgres; Password = masterkey; Database = postgres;");
            con.Open();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO supplier (name) VALUES (:name)", con);
            command.Parameters.AddWithValue("name", name);
            command.ExecuteNonQuery();
            con.Close();
            Close();
        }
    }
}
