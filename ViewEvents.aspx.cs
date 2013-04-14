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

public partial class ViewEvents : System.Web.UI.Page
{
    /*Class memebers*/

    SqlCommand com;
    SqlDataReader reader;
    static int rowid=1;
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();

    /*
     * protected override void OnInit(EventArgs e)
     * The Event handler that gets invoked automatically on the envent of page creation
     * @Overrides the base class method OnInit
     * @params: EventArgs e is the set of arguments that is passed on teh creation of teh page
     * purpose: It loads the latest event details
     */
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        LabelRowId.Text = "" + rowid;
        PopulateCurrentEventDetails();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    /**
     * void LinkButtonPrevious_Click(object sender, EventArgs e)
     * An event handler
     * Arguments and parameters follows usual even handler protoctypes
     * It shows the previous event to the selected event
     */
    protected void LinkButtonPrevious_Click(object sender, EventArgs e)
    {
        rowid++;
        LabelRowId.Text = "" + rowid;
        PopulateCurrentEventDetails();
    }

    /**
     * void LinkButtonNext_Click(object sender, EventArgs e)
     * An event handler
     * Arguments and parameters follows usual even handler protoctypes
     * It shows the next event to the selected event
     */
    protected void LinkButtonNext_Click(object sender, EventArgs e)
    {
        if (rowid == 1) //If it is the latest event there no Next event, so do nothing
        {
            return;
        }
        rowid--;
        LabelRowId.Text = "" + rowid;
        PopulateCurrentEventDetails();
    }
    /*
     * void PopulateCurrentEventDetails()
     * @params none
     * @return none
     * Populates all the details related to the selected event
    */

    protected void PopulateCurrentEventDetails()
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            //Creates SQL connection
            int rowid = Int16.Parse(LabelRowId.Text);

            con.Open();
            com = new SqlCommand("SELECT * FROM EVENTS_VIEW WHERE ROW_ID=@rowid", con);
            com.Parameters.Add("@rowid", SqlDbType.Int).Value = rowid;
            reader = com.ExecuteReader();
            String eventid = "";
            String eventlocation = "";
            String eventdate = "";
            String eventname = "";
            if (reader.Read())
            {
                eventid = reader["EVENT_ID"].ToString();
                eventname = reader["EVENT_NAME"].ToString();
                eventlocation = reader["EVENT_LOCATION"].ToString();
                eventdate = ((DateTime)reader["EVENT_DATE"]).ToShortDateString();

            }
            reader.Close();

            LabelName.Text = eventname;
            LabelDate.Text = eventdate;
            LabelPlace.Text = eventlocation;
            if (string.IsNullOrEmpty(eventid))
            {
                TableImages.Rows.Clear();
                return;
            }
            int eventId = Int16.Parse(eventid);
            com = new SqlCommand("SELECT ALBUM_ENTRY_ID FROM EVENT_ALBUM WHERE EVENT_ID=@eventId", con);
            com.Parameters.Add("@eventId", SqlDbType.Int).Value = eventId;
            reader = com.ExecuteReader();

            int column = 0;
            TableImages.Rows.Clear();
            TableRow r = new TableRow();
            while (reader.Read())
            {
                string entryId = reader["ALBUM_ENTRY_ID"].ToString();
                TableCell c = new TableCell();
                c.VerticalAlign = VerticalAlign.Middle;
                ImageButton i = new ImageButton();
                i.Width = Unit.Pixel(160);
                i.ImageUrl = "GetImage.aspx?imageId=" + entryId;
                i.ID = "Button_" + entryId;
                i.Click += new ImageClickEventHandler(this.Image_Click);
                c.Controls.Add(i);
                r.Cells.Add(c);
                column++;
                if (column == 3)
                {
                    TableImages.Rows.Add(r);
                    column = 0;
                    r = new TableRow();
                }
                
            }
            if (column != 0)
            {
                TableImages.Rows.Add(r);
            }
        }
    }

    /*
     * void Image_Click(object sender, EventArgs e)
     * Event handler to be invoked on clicking on a tubnail of the photgraph
     * void Image_Click(object sender, EventArgs e)
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method opens up a large view of the image chosen
     * */

    protected void Image_Click(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        string id = button.ID;
        string imageid = id.Split("_".ToCharArray())[1];
        PanelImage.Visible = true;
        Image1.ImageUrl = "GetImage.aspx?imageId=" + imageid;
    }

    /*
     * void ImageButtonClose_Click(object sender, EventArgs e)
     * Event handler to be invoked on clicking on the close button on the large image display
     * void Image_Click(object sender, EventArgs e)
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method closes the overlay image display
     * */
    protected void ImageButtonClose_Click(object sender, ImageClickEventArgs e)
    {
        PanelImage.Visible = false;
    }
}
