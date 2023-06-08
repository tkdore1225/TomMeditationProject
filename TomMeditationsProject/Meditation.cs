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
    public partial class Meditation : Form
    {
        public Meditation()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        int ID;
        int rowID;

        private void Meditation_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Select * from tb_Meditation", con);
            cmd.Connection = con;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            dgvMeditation.DataSource = ds.Tables[0];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Insert into tb_Meditation values(@FullName, @MemberID, @Duration, @Location, @Rate)", con);

            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
            cmd.Parameters.AddWithValue("@Rate", txtRate.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Meditation record added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Meditation_Load(null, null);
            clear();
        }

        private void clear()
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            SqlCommand cmd = new SqlCommand("Update tb_Meditation set FullName=@FullName, MemberID=@MemberID, Duration=@Duration, Location=@Location, Rate=@Rate where Id = '"+ ID +"'", con);

            con.Open();
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@MemberID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
            cmd.Parameters.AddWithValue("@Rate", txtRate.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Meditation record updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Meditation_Load(this, null);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete the selected record?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Delete from tb_Meditation where Id= '" + ID + "'");
                    cmd.Connection = con;

                    MessageBox.Show("Fruit Data Deleted Successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    Meditation_Load(this, null);
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
            txtDuration.Clear();
            txtLocation.Clear();
            txtRate.Clear();
        }


        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchID.Text != "")
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Meditation where MemberID LIKE '" + txtSearchID.Text + "%'", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    dgvMeditation.DataSource = ds.Tables[0];

                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                    SqlCommand cmd = new SqlCommand("Select * from tb_Meditation", con);
                    cmd.Connection = con;

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    dgvMeditation.DataSource = ds.Tables[0];

                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvMeditation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMeditation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ID = int.Parse(dgvMeditation.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                con.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                SqlCommand cmd = new SqlCommand("Select * from tb_Meditation where Id = '" + ID + "'");
                cmd.Connection = con;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                rowID = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                txtFullName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtMemberID.Text = ds.Tables[0].Rows[0][2].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0][3].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0][4].ToString();
                txtRate.Text = ds.Tables[0].Rows[0][5].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
