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
    public partial class Yoga : Form
    {
        public Yoga()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;

        private void Yoga_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Yoga", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvYoga.DataSource = ds.Tables[0];

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Yoga values(@FullName, @MemberID, @Instructor, @Duration, @Level)", con);
            
            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Instructor", txtInstructor.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
            cmd.Parameters.AddWithValue("@Level", txtLevel.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Yoga record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Yoga_Load(null, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Yoga set FullName=@Fullname, MemberID=@MemberID, Instructor=@Instructor, Duration=@Duration, Level=@Level where Id = '" + ID + "'", con);

            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Instructor", txtInstructor.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
            cmd.Parameters.AddWithValue("@Level", txtLevel.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Yoga record Updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Yoga_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Yoga where Id = '" + ID + "'");
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    MessageBox.Show("Yoga Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Yoga_Load(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtFullName.Clear();
            txtMemberID.Clear();
            txtInstructor.Clear();
            txtDuration.Clear();
            txtLevel.Clear();
        }


        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchID.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Yoga where MemberID LIKE '" + txtMemberID.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvYoga.DataSource = ds.Tables[0];
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Yoga", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvYoga.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvYoga_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvYoga.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvYoga.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Select * from tb_Yoga where Id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtFullName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtMemberID.Text = ds.Tables[0].Rows[0][2].ToString();
                txtInstructor.Text = ds.Tables[0].Rows[0][3].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0][4].ToString();
                txtLevel.Text = ds.Tables[0].Rows[0][5].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
