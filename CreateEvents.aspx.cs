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

public partial class CreateEvents : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();
    SqlCommand com;
    SqlDataReader reader;
    int parishid = 1;

    /*
     * protected override void OnInit(EventArgs e)
     * The Event handler that gets invoked automatically on the envent of page creation
     * @Overrides teh base class method
     * @params: EventArgs e is the set of arguments that is passed on teh creation of teh page
     * purpose: populate the Date control drop down menus in the UI with appropriate set of date
     */
    protected override void OnInit(EventArgs e)
    {
        //Invokes the base class method
        base.OnInit(e);
        populateDateControls(DropDownYear, DropDownMonth, DropDownDay);
    }

    /*
     * void Page_Load(object sender, EventArgs e)
     * Event handler that gets invoked automatically in the event of loading the WebPage
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //As of now we are doing nothing on page load
    }


    /*
     * void populateDateControls(DropDownList yr, DropDownList month, DropDownList day)
     * @params: DropDownList yr
     * @params: DropDownList month
     * @params: DropDownList day
     * @reutn: none
     * This method takes the UI drop down conponents for yearr month and day, and populate the drop downs with values for selection
     * This method delegates control to populateDays() for polulating days
     */
    protected void populateDateControls(DropDownList yr, DropDownList month, DropDownList day)
    {
        int startYr = DateTime.Now.Year;
        for (int i = 0; i < 2; i++)
        {
            yr.Items.Add("" + (startYr + i));
        }
        month.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            month.Items.Add("" + i);
        }
        populeDays(yr, month, day);
    }


    /*
     * void populeDays(DropDownList yr, DropDownList month, DropDownList day)
     * @params: DropDownList yr
     * @params: DropDownList month
     * @params: DropDownList day
     * @reutn: none
     * Populates th day drop down with the  integers upto number of datys in teh month depending on the
     * selected year and month
     */
    protected void populeDays(DropDownList yr, DropDownList month, DropDownList day)
    {
        int daysInMonth = DateTime.DaysInMonth(Int16.Parse(yr.SelectedItem.Text), Int16.Parse(month.SelectedItem.Text));
        day.Items.Clear();
        for (int i = 1; i <= daysInMonth; i++)
        {
            day.Items.Add("" + i);
        }
    }

    /*
     * void DropDownYear_SelectedIndexChanged(object sender, EventArgs e)
     * Event handler that gets invoked when the selected content of the Year drop down changes
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     */
    protected void DropDownYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownYear, DropDownMonth, DropDownDay);
    }

    /*
     * void DropDownMonth_SelectedIndexChanged(object sender, EventArgs e)
     * Event handler that gets invoked when the selected content of the Month drop down changes
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     */
    protected void DropDownMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownYear, DropDownMonth, DropDownDay);
    }

    /*
     * void ButtonAddPhotos_Click(object sender, EventArgs e)
     * Event handler that gets invoked on clicking the button Add Photos button
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * purpose: Creates a new entry in EVENTS table and shows the template to add photographs for the image
     */
    protected void ButtonAddPhotos_Click(object sender, EventArgs e)
    {
        String eventname = TextName.Text;
        String eventlocation = TextPlace.Text;
        int yr = Int16.Parse(DropDownYear.SelectedItem.Text);
        int month = Int16.Parse(DropDownMonth.SelectedItem.Text);
        int day = Int16.Parse(DropDownDay.SelectedItem.Text);
        DateTime eventdate = new DateTime(yr, month, day);
        String description = TextDescription.Text;

        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("INSERT INTO EVENTS(PARISH_ID,EVENT_NAME,EVENT_LOCATION,EVENT_DATE,EVENT_DESCRIPTION)VALUES(@parishid,@eventname,@eventlocation,@eventdate,@description)", con);
            com.Parameters.Add("@parishid", SqlDbType.Int).Value = parishid;
            com.Parameters.Add("@eventname", SqlDbType.VarChar).Value = eventname;
            com.Parameters.Add("@eventlocation", SqlDbType.VarChar).Value = eventlocation;
            com.Parameters.Add("@eventdate", SqlDbType.DateTime).Value = eventdate;
            com.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
            con.Open();
            com.ExecuteNonQuery();
            LabelMessage2.Text = "";
            LabelEventName.Text = eventname;
            Panel1.Visible = false;
            Panel2.Visible = true;
        }
    }

    /*
     * void ButtonCreateNewEvent_Click(object sender, EventArgs e)
     * * Event handler that gets invoked on clicking the button Add New Event
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * purpose: shows the add event template to add a new event
     */
    protected void ButtonCreateNewEvent_Click(object sender, EventArgs e)
    {
        TextName.Text = "";
        TextPlace.Text = "";
        TextDescription.Text = "";
        Panel1.Visible = true;
        Panel2.Visible = false;
    }

    /*
     * void ButtonAddImage_Click(object sender, EventArgs e)
     * Event handler that gets invoked on clicking the button Add Photo button
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * purpose: Adds the selected photograph to EVENT_ALBUM table
     */
    protected void ButtonAddImage_Click(object sender, EventArgs e)
    {
        byte[] imagedata = FileUploadImage.FileBytes;
        
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            int eventid = 0;
            com = new SqlCommand("SELECT MAX(EVENT_ID) ID FROM EVENTS", con);
            con.Open();
            reader=com.ExecuteReader();
            if (reader.Read())
            {
                String s = reader["ID"].ToString();
                eventid=Int16.Parse(s);
            }
            reader.Close();
            com = new SqlCommand("INSERT INTO EVENT_ALBUM(EVENT_ID,IMAGE_DATA)VALUES(@eventid,@imagedata)", con);
            com.Parameters.Add("@eventid", SqlDbType.Int).Value = eventid;
            com.Parameters.Add("@imagedata", SqlDbType.VarBinary).Value = imagedata;
            com.ExecuteNonQuery();
            LabelMessage2.Text = "Successfully added photos";
        }
    }
}