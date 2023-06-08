using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomMeditationsProject
{
    public partial class Prayer : Form
    {
        public Prayer()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;

        private void Prayer_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Prayer", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvPrayer.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Prayer values(@FullName, @MemberID, @Gratitude)", con);

            con.Open();

            cmd.Parameters.AddWithValue("@Fullname", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Gratitude", txtGratitude.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Prayer record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Prayer_Load(this, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Prayer where Id = '" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("Prayer Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    Prayer_Load(this, null);
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
            SqlCommand cmd = new SqlCommand("Update tb_Prayer set FullName=@FullName, MemberID=@MemberID, Gratitude=@Gratitude where Id = '"+ ID +"'", con);
            
            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("Gratitude", txtGratitude.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Prayer record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Prayer_Load(this, null);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtFullName.Clear();
            txtMemberID.Clear();
            txtGratitude.Clear();
        }


        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchID.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Prayer where MemberID LIKE '" + txtSearchID + "'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvPrayer.DataSource = ds.Tables[0];
                }
                else
                {
                    SqlConnection con = new SqlConnection();

                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Prayer", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvPrayer.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvPrayer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvPrayer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvPrayer.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Select * from tb_Prayer where Id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtFullName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtMemberID.Text = ds.Tables[0].Rows[0][2].ToString();
                txtGratitude.Text = ds.Tables[0].Rows[0][3].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
