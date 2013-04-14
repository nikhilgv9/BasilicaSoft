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
using System.Data.SqlTypes;

public partial class Announcements : System.Web.UI.Page
{
    /* Class memebers */
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();
    SqlDataReader reader;
    SqlCommand com;

    /*
     * void Page_Load(object sender, EventArgs e)
     * Event method that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * purpose: In case any particular Announcement is requested, display that announcement only
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            if (Request["announcementid"] != null)
            {
                String id = Request["announcementid"].ToString();
                com = new SqlCommand("SELECT * FROM ANNOUNCEMENTS  WHERE ANNOUNCEMENT_ID = @Announcementid", con);
                com.Parameters.Add("@Announcementid", SqlDbType.Int).Value = id;
            }
            else
            {
                return;
            }
           
            con.Open();
            pupulateAnnouncementDetails();
            con.Close(); 
        }
     }


    /*
     *  void ImageButtonPrayer_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Prayer type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Display announcements of Prayer Category
     */
    protected void ImageButtonPrayers_Click(object sender, ImageClickEventArgs e)
    {
        LabelType.Text = "Prayers";
        showAnnouncements("PRAYERS");
    }



    /*
     *  void ImageButtonMeetings_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Meetings type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Display announcements of Meetings Category
     */

    protected void ImageButtonMeetings_Click(object sender, ImageClickEventArgs e)
    {
        LabelType.Text = "Meetings";
        showAnnouncements("MEETINGS");
    }


    /*
     *  void ImageButtonCatheism_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Catheism type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Display announcements of Catheism Category
     */
    protected void ImageButtonCatheism_Click(object sender, ImageClickEventArgs e)
    {
        LabelType.Text = "Catheism";
        showAnnouncements("CATHEISM");
    }


    /*
     *  ImageButtonOthers_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Other type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Display announcements of Other Category
     */
    protected void ImageButtonOthers_Click(object sender, ImageClickEventArgs e)
    {
        LabelType.Text = "Others";
        showAnnouncements("OTHERS");
    }

    /*
     * void showAnnouncements(string type)
     * @params: type is a string that mentions the type of the announcement to be displayed
     * @return: none
     * purpose: Retrieves the details of the Announcements belonging to a particular category from database
     */
    protected void showAnnouncements(string type)
    {
        string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("SELECT * FROM ANNOUNCEMENTS  WHERE TYPE=@type AND  (GETDATE() BETWEEN DATE_FROM AND DATE_TO) ORDER BY ANNOUNCEMENT_ID DESC", con);
            com.Parameters.Add("@type", SqlDbType.VarChar).Value = type;

            con.Open();
            pupulateAnnouncementDetails();
            con.Close();
        }
    }

    /*
     * void pupulateAnnouncementDetails()
     * @params: none
     * @return: none
     * purpose: Displays the deatiled announcement
     */
    protected void pupulateAnnouncementDetails()
    {
        reader = com.ExecuteReader();
        PanelAnnouncementDetails.Controls.Clear();
        while (!reader.IsClosed && reader.Read())
        {
            String title = reader["TITLE"].ToString();
            String announcement = reader["ANNOUNCEMENT_DESCRIPTION"].ToString();

            Label h4 = new Label();
            h4.Text = title;
            h4.Font.Underline = true;
            h4.CssClass = "h4";
            PanelAnnouncementDetails.Controls.Add(h4);
            HtmlControl br = new HtmlGenericControl("br");
            PanelAnnouncementDetails.Controls.Add(br);
            Label p = new Label();
            p.Text = announcement;
            PanelAnnouncementDetails.Controls.Add(p);
            br = new HtmlGenericControl("br");
            PanelAnnouncementDetails.Controls.Add(br);
        }
        reader.Close();
    }
}
