using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Health_Care_Pharmacy.Views.Seller
{
    public partial class Billing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowMedicine();
            Show_Bills();       
        }
        private void ShowMedicine()
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_select_med_fetch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Category_List.DataSource = ds.Tables[0];
            Category_List.DataBind();
            con.Close();
        }
        public void InsertDataIntoBillTblAndUpdateMedicineTbl(string product, int price, int quantity, string billDate)
        {
            string connectionString = "Data Source=DESKTOP-G5NSBD9\\SQLEXPRESS01;Initial Catalog=pharmacry_db;Integrated Security=True"; // Replace with your connection string

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertIntoBillTblAndUpdateMedicineTbl", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Product", product);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Bill_Date", billDate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Show_Bills()
        {
            string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
            SqlConnection con = new SqlConnection(projectConnection);
            con.Open();
            SqlCommand cmd = new SqlCommand("Get_Medicine_Data", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Bill_List.DataSource = ds.Tables[0];
            Bill_List.DataBind();
            con.Close();
            int Grand_Total = 0;
            for (int i = 0; i < Bill_List.Rows.Count; i++)
            {
                Grand_Total += Convert.ToInt32(Bill_List.Rows[i].Cells[6].Text);
            }
            Grand_Lable.Text="RS " + Grand_Total;
        }
        protected void Category_List_SelectedIndexChanged(object sender, EventArgs e)
        {
           Med_Code_Txt.Text = Category_List.SelectedRow.Cells[2].Text;
           Med_Name_Txt.Text = Category_List.SelectedRow.Cells[3].Text;
           Med_Quantity_Txt.Text = Category_List.SelectedRow.Cells[4].Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Med_Code_Txt.Text= "";
            Med_Name_Txt.Text = "";
            Med_Quantity_Txt.Text = "";
            Bill_Date_Txt.Text = "";
        }
        protected void DeleteBill()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-G5NSBD9\\SQLEXPRESS01;Initial Catalog=pharmacry_db;Integrated Security=True"; // Replace with your actual connection string
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_delete_bill", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
           
                string projectConnection = ConfigurationManager.ConnectionStrings["Pharmacy_Tracking"].ConnectionString;
                SqlConnection con = new SqlConnection(projectConnection);
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertIntoBillTblAndUpdateMedicineTbl", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@Product", SqlDbType.VarChar);
                cmd.Parameters.Add(param1).Value = Med_Code_Txt.Text;
                SqlParameter param2 = new SqlParameter("@Price", SqlDbType.Int);
                cmd.Parameters.Add(param2).Value = Med_Name_Txt.Text;
                SqlParameter param3 = new SqlParameter("@Quantity", SqlDbType.Int);
                cmd.Parameters.Add(param3).Value = Med_Quantity_Txt.Text;
                SqlParameter param4 = new SqlParameter("@Bill_Date", SqlDbType.VarChar);
                cmd.Parameters.Add(param4).Value = Bill_Date_Txt.Text;
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    Response.Write("Data Inserted Sucessfully");
                    Show_Bills();
                }
                else
                {
                    Response.Write("Data Insertion Failed");
                }
                con.Close();
            
        }
        protected void GeneratePDFReport()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-G5NSBD9\\SQLEXPRESS01;Initial Catalog=pharmacry_db;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Bill_Tbl";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    Document document = new Document();
                    PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/BillReport.pdf"), FileMode.Create));
                    document.Open();

                    PdfPTable table = new PdfPTable(dt.Columns.Count);

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dt.Columns[i].ColumnName));
                        table.AddCell(cell);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            table.AddCell(dt.Rows[i][j].ToString());
                        }
                    }
                    document.Add(table);
                    document.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            GeneratePDFReport();
            string filePath = Server.MapPath("~/BillReport.pdf");
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/pdf";
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            else
            {
                Response.Write("Pdf not generated");
            }
            GeneratePDFReport();
            Response.Redirect("BillReport.pdf");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            DeleteBill();
        }
    }
}