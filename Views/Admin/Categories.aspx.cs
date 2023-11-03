using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Health_Care_Pharmacy.Views.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCategories();
        }
        private void ShowCategories()
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_cat_fetch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Categories_List.DataSource = ds.Tables[0];
            Categories_List.DataBind();
            con.Close();
        }
        protected void Medicine_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cat_Id_Txt.Text = Categories_List.SelectedRow.Cells[1].Text;
            Cat_Name_Txt.Text = Categories_List.SelectedRow.Cells[2].Text;
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_cat_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@cat_name", SqlDbType.VarChar);
                cmd.Parameters.Add(param1).Value = Cat_Name_Txt .Text;
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    Error_Message.Text = "Data Inserted Sucessfully";
                    ShowCategories();
                }
                else
                {
                    Error_Message.Text = "Data Inserted Failed";
                }
                con.Close(); 
            }

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        protected void Edit_Btn_Click(object sender, EventArgs e)
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_cat_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@cat_id", SqlDbType.Int);
            cmd.Parameters.Add(param1).Value = Cat_Id_Txt .Text;
            SqlParameter param2 = new SqlParameter("@cat_name", SqlDbType.VarChar);
            cmd.Parameters.Add(param2).Value = Cat_Name_Txt.Text;
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Error_Message.Text = "Data Updated Sucessfully";
                ShowCategories();
            }
            else
            {
                Error_Message.Text = "Data Updated Failed";
            }
            con.Close();
        }
        protected void Delete_Btn_Click1(object sender, EventArgs e)
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_cat_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@cat_id", SqlDbType.Int);
            cmd.Parameters.Add(param1).Value = Cat_Id_Txt.Text;
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Error_Message.Text = "Data Deleted Sucessfully";
                ShowCategories();
            }
            else
            {
                Error_Message.Text = "Data Deletion Failed";
            }
            con.Close();
        }
    }
}