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
        SqlDataReader reader;
        public int empid;
       // public int fid;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
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
                string sql = "INSERT INTO EmployeeInfo(staffID,Name,Surname,Address,Age,Gender,Role,Photo)VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "','" + textBox6.Text + "', " + textBox4.Text + ", '" + comboBox1.Text + "', '" + comboBox2.Text + "',@images)";
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("@images", images));
                int x = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x.ToString() + "record(s) saved");
                BindData();

                




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

        void BindData()
        {
            String query = "SELECT * FROM EmployeeInfo";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text =textBox3.Text=textBox4.Text=textBox6.Text=comboBox1.Text=comboBox2.Text= textBox4.Text = "";
            image1.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE EmployeeInfo SET Name='" + textBox2.Text + "',Surname='" + textBox3.Text + "',Age='" + textBox4.Text + "',Gender='"+comboBox1.Text+ "',Role='" + comboBox2.Text + "',Address='" + textBox6.Text + "' WHERE staffID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", this.empid);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Update Data Susscessfully");
            con.Close();
            BindData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comd = new SqlCommand("DELETE FileInfo WHERE staffID='" + textBox1.Text + "'", con);
            comd.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("DELETE EmployeeInfo WHERE staffID='"+textBox1.Text+"'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted Data Susscessfully");
            con.Close();
            BindData();
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

        public void UploadFile(string file)
        {
            con.Open();
            FileStream fstream = File.OpenRead(file);
            byte[] contents = new byte[fstream.Length];
            fstream.Read(contents, 0, (int)fstream.Length);
            fstream.Close();
            using (cmd = new SqlCommand("INSERT INTO FileInfo(staffID,Files) VALUES(@staffID,@Files) ",con))
            {
               // cmd.Parameters.AddWithValue("@FID", this.fid);
                cmd.Parameters.AddWithValue("@staffID", textBox1.Text);
                cmd.Parameters.AddWithValue("@Files", contents);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("File uploaded","uploaded",MessageBoxButtons.OK,MessageBoxIcon.Information);
            BindData();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog() { Filter = "Text Documents (*.pdf;) |*.pdf", ValidateNames = true })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DialogResult dialog = MessageBox.Show("Are you sure you want to upload file?", "upload box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    string filename = dlg.FileName;
                    UploadFile(filename);
                }
                else
                {
                    return;
                }
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            String query = "SELECT * FROM EmployeeInfo";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Search
            con.Open();
            cmd = new SqlCommand("select * from EmployeeInfo where Name like'" + textBox2.Text + "%' order by Name",con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds, "EmployeeInfo");
            dataGridView1.DataSource = ds.Tables["EmployeeInfo"].DefaultView;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            empid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }
        Bitmap bitmap;
        private void button9_Click(object sender, EventArgs e)
        {
            int height = dataGridView1.Height;
            dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
            bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            dataGridView1.Height = height;
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
