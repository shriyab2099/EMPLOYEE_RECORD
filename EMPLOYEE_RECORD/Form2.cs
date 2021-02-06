using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EMPLOYEE_RECORD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\EMPLOYEE_RECORD\EMPLOYEE_RECORD\EmployeeDatabase.mdf;Integrated Security=True");
            string query = "Select * from LoginInfo Where Username= '" + textBox1.Text.Trim() + "' and Password='" + textBox2.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dbtl = new DataTable();
            sda.Fill(dbtl);
            if (dbtl.Rows.Count == 1)
            {
                Form1 objMainform = new Form1();
                this.Hide();
                objMainform.Show();
            }
            else
            {
                MessageBox.Show("Wrong Username or password");
            }

        }

       
    }
}
