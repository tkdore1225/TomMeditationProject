using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomMeditationsProject
{
    public partial class Mindfulness : Form
    {
        public Mindfulness()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;

        private void Mindfulness_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Mindfulness", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvMindfulness.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Mindfulness values(@FullName, @MemberID, @Description, @Date)", con);

            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Mindfulness recorded successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Mindfulness_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete * from tb_Mindfulness where Id='" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("Mindfulness Data Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Mindfulness_Load(this, null);

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
            SqlCommand cmd = new SqlCommand("Update tb_Mindfulness set FullName=@FullName, MemberID=@MemberID, Description=@Description, Date=@Date where Id = '" + ID + "'", con);

            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@Date", txtDate.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Mindfulness record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Mindfulness_Load(this, null);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }


        private void ClearAll()
        {
            txtFullName.Clear();
            txtMemberID.Clear();
            txtDescription.Clear();
            txtDate.Clear();
        }


        private void dgvMindfulness_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMindfulness.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvMindfulness.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Select * from tb_Mindfulness where Id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                txtFullName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtMemberID.Text = ds.Tables[0].Rows[0][2].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0][3].ToString();
                txtDate.Text = ds.Tables[0].Rows[0][4].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void txtSearchDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDate.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Mindfulness where Date LIKE '" + txtSearchDate.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvMindfulness.DataSource = ds.Tables[0];
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Mindfulness", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvMindfulness.DataSource = ds.Tables[0];
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
                    SqlCommand cmd = new SqlCommand("Select * from tb_Mindfulness where MemberID LIKE '" + txtSearchID.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvMindfulness.DataSource = ds.Tables[0];
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Mindfulness", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvMindfulness.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

