using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace EMPLOYEE_RECORD
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\EMPLOYEE_RECORD\EMPLOYEE_RECORD\EmployeeDatabase.mdf;Integrated Security=True");
        String imageLocation = "";
        SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] images = null;
                FileStream Streem = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Streem);
                images = brs.ReadBytes((int)Streem.Length);
                string sql = "INSERT INTO EmployeeInfo(staffID,Name,Surname,Age,Gender,Role,Photo)VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + comboBox1.Text + "', '" + comboBox2.Text + "',@images)";
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@images", images));
                int x = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + "record(s) saved");

                




            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }




            /*con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO EmployeeInfo VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "')", images, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Insert Data Successfully");
            con.Close();*/



        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE EmployeeInfo SET Name='" + textBox2.Text + "',Surname='" + textBox3.Text + "',Age='" + textBox4.Text + "' WHERE staffID='"+textBox1.Text+"', con");
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update Data Susscessfully");
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE EmployeeInfo WHERE staffID='"+textBox1.Text+"'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted Data Susscessfully");
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = dialog.FileName.ToString();

                    image1.ImageLocation = imageLocation;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An Error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
