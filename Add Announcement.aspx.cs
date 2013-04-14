using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


/*
 * Add Announcement.aspx.cs
 * This is the class corresponding to page which is used to create announcements
 */

public partial class AddAnnouncement : System.Web.UI.Page
{
    /*Class memebers*/

    SqlCommand com;
    SqlDataReader reader;
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();


    /*
     * void Page_Load(object sender, EventArgs e)
     * Event method that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //At present we are doing nothing on page load
    }


    /*
     * void ButtonPost_Click(object sender, EventArgs e)
     * Event method that gets invoked when the Post announcement button is clicked 
     * (To post the announcement)
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: Inserts teh announcement into the database when the post button is clicked
     */
    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        //Code to post a new announcement
        int parishId = 1;
        string type;
        string title = TextTitle.Text;
        string message = TextMessage.Text;
        if (string.Equals(TextType.Text, "Prayers"))
        {
            type = "PRAYERS";
        }
        else if (string.Equals(TextType.Text, "Meetings"))
        {
            type = "MEETINGS";
        }
        else if (string.Equals(TextType.Text, "Catheism"))
        {
            type = "CATHEISM";
        }
        else if (string.Equals(TextType.Text, "Other"))
        {
            type = "OTHERS";
        }
        else
        {
            type = "";
        }

        int validity = -1;
        if (!string.IsNullOrEmpty(TextValidity.Text))
        {
            validity = Int16.Parse(TextValidity.Text);
        }

        DateTime dateStart = DateTime.Now;
        DateTime dateEnd = DateTime.MaxValue;

        /* Validates the input values */

        if (validity != -1)
        {
            dateEnd = dateStart.AddDays(validity);
        }

        if (string.IsNullOrEmpty(title))
        {
            LabelMessage.Text = "Title cannot be empty";
            return;
        }
        if (string.IsNullOrEmpty(type))
        {
            LabelMessage.Text = "Type cannot be empty";
            return;
        }

        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            //Creates SQL connection
            con.Open();
            com = new SqlCommand(
                "INSERT INTO ANNOUNCEMENTS (PARISH_ID,TITLE,DATE_FROM,DATE_TO,ANNOUNCEMENT_DESCRIPTION,TYPE) VALUES (@parishId,@title,@dateFrom,@dateTo,@message,@type)", con);
            com.Parameters.Add("@parishId", SqlDbType.Int).Value = parishId;
            com.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
            com.Parameters.Add("@dateFrom", SqlDbType.DateTime).Value = dateStart;
            com.Parameters.Add("@dateTo", SqlDbType.DateTime).Value = dateEnd;
            com.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;
            com.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;

            //Executes the insert query
            int result = com.ExecuteNonQuery();

            //In case of success
            if (result == 1)
            {
                LabelMessage.Text = "Successfully posted new announcement";
                TextTitle.Text = "";
                TextMessage.Text = "";
                TextValidity.Text = "";
            }
            else //In case fo error
            {
                LabelMessage.Text = "Failed to post announcement";
            }
            con.Close();
        }
    }

    /*
     *  void ImageButtonPrayer_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Prayer type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: shows the template to insert a prayer caegory announcement
     */

    protected void ImageButtonPrayer_Click(object sender, ImageClickEventArgs e)
    {
        TextType.Text = "Prayers";
        setActive(ImageButtonPrayer);
        setInActive(ImageButtonMeeting);
        setInActive(ImageButtonCatheism);
        setInActive(ImageButtonOther);
        PanelAnnouncementDetails.Visible = true;
        LabelMessage.Text = "";
    }

    /*
     *  void ImageButtonPrayer_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Meeting type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: shows the template to insert a Meeting caegory announcement
     */
    protected void ImageButtonMeeting_Click(object sender, ImageClickEventArgs e)
    {
        TextType.Text = "Meetings";
        setInActive(ImageButtonPrayer);
        setActive(ImageButtonMeeting);
        setInActive(ImageButtonCatheism);
        setInActive(ImageButtonOther);
        PanelAnnouncementDetails.Visible = true;
        LabelMessage.Text = "";
    }


    /*
     *  void ImageButtonPrayer_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Catheism type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: shows the template to insert a Catheism caegory announcement
     */
    protected void ImageButtonCatheism_Click(object sender, ImageClickEventArgs e)
    {
        TextType.Text = "Catheism";
        setInActive(ImageButtonPrayer);
        setInActive(ImageButtonMeeting);
        setActive(ImageButtonCatheism);
        setInActive(ImageButtonOther);
        PanelAnnouncementDetails.Visible = true;
        LabelMessage.Text = "";
    }

    /*
     *  void ImageButtonPrayer_Click(object sender, ImageClickEventArgs e)
     * Event method that gets invoked when the Other type announcement button is clicked 
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Purpose: shows the template to insert a Other caegory announcement
     */
    protected void ImageButtonOther_Click(object sender, ImageClickEventArgs e)
    {
        TextType.Text = "Other";
        setInActive(ImageButtonPrayer);
        setInActive(ImageButtonMeeting);
        setInActive(ImageButtonCatheism);
        setActive(ImageButtonOther);
        PanelAnnouncementDetails.Visible = true;
        LabelMessage.Text = "";
    }

    /*
     * void setActive(ImageButton i)
     * @params: And instance of the image button whichis to be set active
     * @rertun: none
     * Changes the CSS style elements of the image button so it appears active o nthe UI
     */
    protected void setActive(ImageButton i)
    {
        i.BorderColor = Color.Blue;
        i.BorderStyle = BorderStyle.Solid;
        i.BorderWidth = Unit.Pixel(2);
    }


    /*
     * void setInActive(ImageButton i)
     * @params: And instance of the image button whichis to be set inactive
     * @rertun: none
     * Changes the CSS style elements of the image button so it appears inactive o nthe UI
     */
    protected void setInActive(ImageButton i)
    {
        i.BorderColor = Color.Empty;
        i.BorderStyle = BorderStyle.None;
        i.BorderWidth = Unit.Pixel(0);
    }
    
}
