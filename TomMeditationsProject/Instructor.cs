using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomMeditationsProject
{
    public partial class Instructor : Form
    {
        public Instructor()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;


        private void Instructor_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * From tb_Instructor", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvInstructor.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Instructor values (@FullName,@InstructorID,@PhoneNumber,@Email,@Location)", con);
        
            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@InstructorID", txtInstructorID.Text);
            cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);

            cmd.ExecuteNonQuery(); 
            con.Close();
            MessageBox.Show("Instructor record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Instructor_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Instructor where Id='" + ID + "'");
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    MessageBox.Show("Instructor Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Instructor_Load(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Instructor set FullName=@FullName, InstructorID=@InstructorID, PhoneNumber=@PhoneNumber, Email=@Email, Location=@Location where Id= '" + ID + "'", con);
            con.Open();

            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@InstructorID", txtInstructorID.Text);
            cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Instructor record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Instructor_Load(this, null);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtFullName.Clear();
            txtInstructorID.Clear();
            txtPhoneNumber.Clear();
            txtEmail.Clear();
            txtLocation.Clear();
        }


        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchName.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Instructor where FullName LIKE '" + txtFullName.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvInstructor.DataSource = ds.Tables[0];
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Instructor", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvInstructor.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchID.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Instructor where InstructorID LIKE '" + txtInstructorID.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvInstructor.DataSource = ds.Tables[0];
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Instructor", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvInstructor.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvInstructor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvInstructor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvInstructor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvInstructor.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();

                SqlCommand cmd = new SqlCommand("Select * from tb_Instructor where Id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                //Filling the table data in the respective text boxes
                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtFullName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtInstructorID.Text = ds.Tables[0].Rows[0][2].ToString();
                txtPhoneNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0][4].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0][5].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
