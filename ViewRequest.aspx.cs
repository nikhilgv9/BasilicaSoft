using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class ViewRequest : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();
    SqlDataReader reader;
    SqlCommand com;


    /*
     * void Page_Load(object sender, EventArgs e)
     * Event method that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * purpose: populates the table of requests
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        populateTable();
    }

    /*
     * void populateTable()
     * @params: none
     * @return: none
     * This method populates the list of requests from the user for the administrator to approve
     * The user can see the requests he has made and see its status, whether approved, rjected or pending
     */
    protected void populateTable()
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("SELECT * FROM REQUEST_VIEW WHERE USER_NAME=@username ORDER BY STATUS DESC,BOOKING_DATE", con);
            con.Open();
            string username = HttpContext.Current.User.Identity.Name;
            com.Parameters.Add("@username", SqlDbType.VarChar).Value = username ;
            SqlDataReader reader = com.ExecuteReader();
            TableRow t = new TableRow();
            Array controlls = Array.CreateInstance(t.GetType(), 100);
            Table1.Rows.CopyTo(controlls, 0);
            Table1.Rows.Clear();
            Table1.Rows.Add(((TableRow)controlls.GetValue(0)));

            while (reader.Read())
            {
                string requestId = reader["REQUEST_ID"].ToString();
                string familyname = reader["FAMILY_NAME"].ToString();
                string bookingdate = ((DateTime)reader["BOOKING_DATE"]).ToShortDateString();
                string title = reader["TITLE"].ToString();
                string status = reader["STATUS"].ToString();
                TableRow r = new TableRow();
                TableCell c = new TableCell();
                c.Text = familyname;
                r.Cells.Add(c);
                c = new TableCell();
                c.Text = title;
                r.Cells.Add(c);
                c = new TableCell();
                c.Text = bookingdate;
                r.Cells.Add(c);
                c = new TableCell();
                if (status.Equals("APPROVED"))
                {
                    Label b = new Label();
                    b.Text = "Approved";
                    b.Width = Unit.Pixel(30);
                    b.Height = Unit.Pixel(30);
                    c.Controls.Add(b);
                }
                else if (status.Equals("REJECTED"))
                {
                    Label b = new Label();
                    b.Text = "Rejected";
                    b.Width = Unit.Pixel(30);
                    b.Height = Unit.Pixel(30);
                    c.Controls.Add(b);
                }
                else
                {
                    Label b = new Label();
                    b.Text = "Not Approved";
                    b.Width = Unit.Pixel(30);
                    b.Height = Unit.Pixel(30);
                    c.Controls.Add(b);
                }
                r.Cells.Add(c);
                Table1.Rows.Add(r);
            }
            reader.Close();
        }
    }
}

