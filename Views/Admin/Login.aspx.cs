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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Btn_Click(object sender, EventArgs e)
        {
            if(Email_Txt.Text=="Admin123@gmail.com" && Pwd_Txt.Text=="Admin123")
            {
                Response.Redirect("Medicines.aspx");
            }
            else
            {
                ErrMsg.Text = "Invalid Email or Password";
            }
        }
    }
}