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
using System.Diagnostics;
/*
 * ApproveRequest.aspx.cs
 * This class corresponding to the page from which Administrator approves the requests
 * It creates a tabular display of unapproved requests, where admin can choose approve each request
 */
public partial class ApproveRequest : System.Web.UI.Page
{
    /* Class valiables */
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
     * void ButtonA_Click(object sender, EventArgs e)
     * This is an even method that gets invoked on clicking any Approve button on the
     * requests table
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * purpose: Approves the selected request
     */
    protected void ButtonA_Click(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        string id=button.ID;
        string requestid = id.Split("_".ToCharArray())[1];
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("UPDATE REQUEST SET STATUS='APPROVED' WHERE REQUEST_ID=@requestid ", con);
            con.Open();
            com.Parameters.Add("@requestid", SqlDbType.Int).Value = requestid;
            com.ExecuteNonQuery();
        }
        populateTable();
    }


    /*
     * void Button_Click(object sender, EventArgs e)
     * This is an even method that gets invoked on clicking any Reject button on the
     * requests table
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * purpose: Rejects the selected request
     */
    protected void ButtonR_Click(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        string id = button.ID;
        string requestid = id.Split("_".ToCharArray())[1];
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("UPDATE REQUEST SET STATUS='REJECTED' WHERE REQUEST_ID=@requestid ", con);
            con.Open();
            com.Parameters.Add("@requestid", SqlDbType.Int).Value = requestid;
            com.ExecuteNonQuery();
        }
        populateTable();
    }

    /*
     * void populateTable()
     * @params: none
     * @return: none
     * This method populates the list of requests from the user for the administrator to approve
     * Admin can view different request and aprove them as he wish
     */
    protected void populateTable()
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("SELECT * FROM REQUEST_VIEW ORDER BY STATUS DESC,BOOKING_DATE", con);
            con.Open();
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
                    ImageButton b = new ImageButton();
                    b.ImageUrl = "images/button_ok.png";
                    b.ID = "ButtonA_" + requestId;
                    b.Click += new ImageClickEventHandler(this.ButtonA_Click);
                    b.Width = Unit.Pixel(30);
                    b.Height = Unit.Pixel(30);
                    c.Controls.Add(b);

                    b = new ImageButton();
                    b.ImageUrl = "images/DeleteRed.png";
                    b.ID = "ButtonR_" + requestId;
                    b.Click += new ImageClickEventHandler(this.ButtonR_Click);
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
