using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Health_Care_Pharmacy.Views.Seller
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Txt_Click(object sender, EventArgs e)
        {
            try
            {
                string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@sel_email", SqlDbType.VarChar);
                cmd.Parameters.Add(param1).Value = Email_Txt.Text;
                SqlParameter param2 = new SqlParameter("@sel_pwd", SqlDbType.VarChar);
                cmd.Parameters.Add(param2).Value = Pwd_Txt.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int a = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                if (a > 0)
                {
                    Response.Redirect("Billing.aspx");
                }
                else
                {
                    Response.Write("InValid User");
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                ErrMsg.Text = Ex.Message;
            }
        }
    }
}