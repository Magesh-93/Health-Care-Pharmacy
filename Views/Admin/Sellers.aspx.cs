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
    public partial class Sellers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowSellers();
        }
        private void ShowSellers()
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_sel_fetch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Sellers_List.DataSource = ds.Tables[0];
            Sellers_List.DataBind();
            con.Close();
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_sel_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@sel_name", SqlDbType.VarChar);
                cmd.Parameters.Add(param1).Value = Sel_Name_Txt.Text;
                SqlParameter param2 = new SqlParameter("@sel_email", SqlDbType.VarChar);
                cmd.Parameters.Add(param2).Value = Sel_Email_Txt.Text;
                SqlParameter param3 = new SqlParameter("@sel_pwd", SqlDbType.VarChar);
                cmd.Parameters.Add(param3).Value = Sel_Pwd_Txt.Text;
                SqlParameter param4 = new SqlParameter("@sel_dob", SqlDbType.Date);
                cmd.Parameters.Add(param4).Value = sel_Dob_Txt.Text;
                SqlParameter param5 = new SqlParameter("@sel_gender", SqlDbType.VarChar);
                cmd.Parameters.Add(param5).Value = Sel_Gender_Txt.Text;
                SqlParameter param6 = new SqlParameter("@sel_address", SqlDbType.VarChar);
                cmd.Parameters.Add(param6).Value = Sel_Address_Txt.Text;
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    Error_Message.Text = "Data Inserted Sucessfully";
                    ShowSellers();
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
            SqlCommand cmd = new SqlCommand("sp_sel_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@sel_id", SqlDbType.VarChar);
            cmd.Parameters.Add(param1).Value = Sel_Id_Txt.Text;
            SqlParameter param2 = new SqlParameter("@sel_name", SqlDbType.VarChar);
            cmd.Parameters.Add(param2).Value = Sel_Name_Txt.Text;
            SqlParameter param3 = new SqlParameter("@sel_email", SqlDbType.VarChar);
            cmd.Parameters.Add(param3).Value = Sel_Email_Txt.Text;
            SqlParameter param4 = new SqlParameter("@sel_pwd", SqlDbType.VarChar);
            cmd.Parameters.Add(param4).Value = Sel_Pwd_Txt.Text;
            SqlParameter param5 = new SqlParameter("@sel_dob", SqlDbType.Date);
            cmd.Parameters.Add(param5).Value = sel_Dob_Txt.Text;
            SqlParameter param6 = new SqlParameter("@sel_gender", SqlDbType.VarChar);
            cmd.Parameters.Add(param6).Value = Sel_Gender_Txt.Text;
            SqlParameter param7 = new SqlParameter("@sel_address", SqlDbType.VarChar);
            cmd.Parameters.Add(param7).Value = Sel_Address_Txt.Text;

            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Error_Message.Text = "Data Updated Sucessfully";
                ShowSellers();
            }
            else
            {
                Error_Message.Text = "Data Updated Failed";
            }
            con.Close();
        }

        protected void Delete_Btn_Click(object sender, EventArgs e)
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_sel_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@sel_id", SqlDbType.VarChar);
            cmd.Parameters.Add(param1).Value = Sel_Id_Txt.Text;
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Error_Message.Text = "Data Deleted Sucessfully";
                ShowSellers();
            }
            else
            {
                Error_Message.Text = "Data Deletion Failed";
            }
            con.Close();
        }

        protected void Medicine_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sel_Id_Txt.Text = Sellers_List.SelectedRow.Cells[1].Text;
            Sel_Name_Txt.Text = Sellers_List.SelectedRow.Cells[2].Text;
            Sel_Email_Txt.Text = Sellers_List.SelectedRow.Cells[3].Text;
            Sel_Pwd_Txt.Text = Sellers_List.SelectedRow.Cells[4].Text;
            sel_Dob_Txt.Text = Sellers_List.SelectedRow.Cells[5].Text;
            Sel_Gender_Txt.Text = Sellers_List.SelectedRow.Cells[6].Text;
            Sel_Address_Txt.Text = Sellers_List.SelectedRow.Cells[7].Text;
        }
    }
}