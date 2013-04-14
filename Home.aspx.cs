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

/*
 * Home.aspx.cs
 * This is the class corresponding to the Home page of the application.
 * This is the default page user gets redirected to when he logs in to the application
 * The Major content of this page are notifications of different type, like announcements, incidents etc
 */

public partial class Home : System.Web.UI.Page
{
    /*Class Members*/
    //Loads the conncetion string from configuration file (web.config)
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

    /*
     * void Page_Load(object sender, EventArgs e)
     * Event method that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Shows the announcement notifications
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        AnnouncmentsNotification();
    }

    /*
     * void AnnouncmentsNotification()
     * @params: none
     * @return: Returns nothing
     * Loads the announcement notifications to content area
     */
    protected void AnnouncmentsNotification()
    { 
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            //Estabilished the connection
            con.Open();
            SqlCommand com = new SqlCommand("Select * from ANNOUNCEMENTS where  (GETDATE() BETWEEN DATE_FROM AND DATE_TO) ORDER BY ANNOUNCEMENT_ID DESC", con);
            //Executed the SQL
            SqlDataReader reader = com.ExecuteReader();
            LiteralControl unorderedList = new LiteralControl();
            unorderedList.Text += @"<ul>";
            //Iterates through the results
            while (reader.Read())
            {
                string title = reader["TITLE"].ToString();
                string id = reader["ANNOUNCEMENT_ID"].ToString();
                unorderedList.Text += @"<li><a href='Announcements.aspx?announcementid=" + id + "'><span>" + title + "</span></a></li>";
                //The above line adds  html <li><a href="Announcements.aspx?announcementid=<%=id %>"><span><%=result %></span></a></li>
            }
            unorderedList.Text += @"</ul>";
            PanelAnnouncementNotification.Controls.Add(unorderedList);
        }
    }

}