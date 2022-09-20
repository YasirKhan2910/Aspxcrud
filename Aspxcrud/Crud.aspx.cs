using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aspxcrud
{
    public partial class Crud : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=YASIR-KHAN;Initial Catalog=ASPX;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddetails();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spStudent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@opcode",11);
            cmd.Parameters.AddWithValue("@FirstName", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@LastName", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@Age", TextBox3.Text.Trim());
            cmd.Parameters.AddWithValue("@Branch", DropDownList1.Text.Trim());
            cmd.Parameters.AddWithValue("@Gender", DropDownList2.Text.Trim());
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                lblmsg.Text = "Successfully Created";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                con.Close();
                binddetails();
                reset();
            }
            else
            {

                lblmsg.Text = "Try again";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                con.Close();
                binddetails();
            }
            
        }

        private void binddetails()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spStudent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@opcode", 41);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }

        public void reset()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            DropDownList1.Text = "-SELECT-";
            DropDownList2.Text = "-SELECT-";
            lblmsg.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            reset();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            con.Open();
            int id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("spStudent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@opcode", 21);
            cmd.Parameters.AddWithValue("@FirstName", ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@LastName", ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@Age", ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@Branch", ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@Gender", ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                lblmsg.Text = "Successfully Submitted";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                con.Close();
                GridView1.EditIndex = -1;
                binddetails();

            }
            else
            {
                lblmsg.Text = "try again";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                con.Close();

            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            con.Open();
            int id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("spStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@opcode", 31);
            cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                lblmsg.Text = "Successfully deleted";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                con.Close();
                binddetails();
                

            }

            else
            {
                lblmsg.Text = "Try again!";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                con.Close();
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            binddetails();
        }


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            binddetails();
        }

    }
}