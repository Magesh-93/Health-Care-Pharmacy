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
    public partial class Medicines : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Med_Category_Txt.Items.Count == 0)
                {
                    string connectionString = "Data Source=DESKTOP-G5NSBD9\\SQLEXPRESS01;Initial Catalog=pharmacry_db;Integrated Security=True"; 
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "SELECT Cat_Id, Cat_Name FROM Category_Tbl";
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        Med_Category_Txt.DataSource = reader;
                        Med_Category_Txt.DataTextField = "Cat_Name";
                        Med_Category_Txt.DataValueField = "Cat_Id";
                        Med_Category_Txt.DataBind();
                        con.Close();
                    }
                }
            }
            ShowMedicine();
        }
        protected void Med_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategoryId = Med_Category_Txt.SelectedValue;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            SqlCommand cmd = new SqlCommand("sp_select_category", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Med_Category_Txt.SelectedValue);
            cmd.Parameters.AddWithValue("@SelectedValue", Med_Category_Txt.SelectedItem.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        private void ShowMedicine()
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_med_fetch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Medicine_List.DataSource = ds.Tables[0];
            Medicine_List.DataBind();
            con.Close();
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_med_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@med_code", SqlDbType.VarChar);
                cmd.Parameters.Add(param1).Value = Med_Code_Txt.Text;
                SqlParameter param2 = new SqlParameter("@med_name", SqlDbType.VarChar);
                cmd.Parameters.Add(param2).Value = Med_Name_Txt.Text;
                SqlParameter param3 = new SqlParameter("@med_price", SqlDbType.Int);
                cmd.Parameters.Add(param3).Value = Med_Price_Txt.Text;
                SqlParameter param4 = new SqlParameter("@med_stock", SqlDbType.Int);
                cmd.Parameters.Add(param4).Value = Med_Stock_Txt.Text;
                SqlParameter param5 = new SqlParameter("@med_exp_date", SqlDbType.Date);
                cmd.Parameters.Add(param5).Value = Med_Date_Txt.Text;
                SqlParameter param6 = new SqlParameter("@med_category", SqlDbType.Int);
                cmd.Parameters.Add(param6).Value = Med_Category_Txt.Text;
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    Error_Message.Text = "Data Inserted Sucessfully";
                    ShowMedicine();
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
            SqlCommand cmd = new SqlCommand("sp_med_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@med_code", SqlDbType.VarChar);
            cmd.Parameters.Add(param1).Value = Med_Code_Txt.Text;
            SqlParameter param2 = new SqlParameter("@med_name", SqlDbType.VarChar);
            cmd.Parameters.Add(param2).Value = Med_Name_Txt.Text;
            SqlParameter param3 = new SqlParameter("@med_price", SqlDbType.Int);
            cmd.Parameters.Add(param3).Value = Med_Price_Txt.Text;
            SqlParameter param4 = new SqlParameter("@med_stock", SqlDbType.Int);
            cmd.Parameters.Add(param4).Value = Med_Stock_Txt.Text;
            SqlParameter param5 = new SqlParameter("@med_exp_date", SqlDbType.VarChar);
            cmd.Parameters.Add(param5).Value = Med_Date_Txt.Text;
            SqlParameter param6 = new SqlParameter("@med_category", SqlDbType.Int);
            cmd.Parameters.Add(param6).Value = Med_Category_Txt.Text;
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Error_Message.Text = "Data Updated Sucessfully";
                ShowMedicine();
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
            SqlCommand cmd = new SqlCommand("sp_med_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@med_code", SqlDbType.VarChar);
            cmd.Parameters.Add(param1).Value = Med_Code_Txt.Text;
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                Error_Message.Text = "Data Deleted Sucessfully";
                ShowMedicine();
            }
            else
            {
                Error_Message.Text = "Data Deletion Failed";
            }
            con.Close();
        }

        protected void Medicine_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            Med_Code_Txt.Text = Medicine_List.SelectedRow.Cells[1].Text;
            Med_Name_Txt.Text = Medicine_List.SelectedRow.Cells[2].Text;
            Med_Price_Txt.Text = Medicine_List.SelectedRow.Cells[3].Text;
            Med_Stock_Txt.Text = Medicine_List.SelectedRow.Cells[4].Text;
            Med_Date_Txt.Text = Medicine_List.SelectedRow.Cells[5].Text;
            Med_Category_Txt.Text = Medicine_List.SelectedRow.Cells[6].Text;
        }
    }
}