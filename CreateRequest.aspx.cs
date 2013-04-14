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
 * CreateRequest.aspx.cs
 * This c# class corresponds to the page that helps the parishioner to create a request
 * for a parish resource
 * 
 */

public partial class CreateRequest : System.Web.UI.Page
{
    /*Class variables */
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();
    SqlDataReader reader;
    SqlCommand com;


    /*
     * protected override void OnInit(EventArgs e)
     * The Event handler that gets invoked automatically on the envent of page creation
     * @Overrides teh base class method
     * @params: EventArgs e is the set of arguments that is passed on teh creation of teh page
     * purpose: populate the Date control drop down menus in the UI with appropriate set of date
     * it slo populates the list of resources
     */
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        TextRequest.Text = "Physical Property";
        populateDateControls(DropDownYear, DropDownMonth, DropDownDay);
        PopulateResource();
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
     * void Image1_Click(object sender, ImageClickEventArgs e)
     * Event handler that gets invoked when the 'Physical Property' UI image Button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Changes the request type to 'Physical Property'
     */

    protected void Image1_Click(object sender, ImageClickEventArgs e)
    {
        TextRequest.Text = "Physical Property";
        PopulateResource();

    }


    /*
     *void Image2_Click(object sender, ImageClickEventArgs e)
     * Event handler that gets invoked when the 'Date' UI ImageButton is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * Changes the request type to 'Date'
     */
    protected void Image2_Click(object sender, ImageClickEventArgs e)
    {
        TextRequest.Text = "Date";
        PopulateResource();
    }

    /*
     * void PopulateResource()
     * @params: none
     * @return: none
     * Populates the UI Drop down list to choose the resources.
     * It queries the PARISH_RESOURCES table and retrieve the resources belonging to a particular type
     */
    protected void PopulateResource()
    {
        DropDownResource.Items.Clear();
        String type = TextRequest.Text;
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            com = new SqlCommand("SELECT * FROM PARISH_RESOURCES WHERE RESOURCE_TYPE=@resourcetype ", con);
            com.Parameters.Add("@resourcetype", SqlDbType.VarChar).Value = type;
            con.Open();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                string title = reader["TITLE"].ToString();
                String resourceid = reader["PARISH_RESOURCE_ID"].ToString();
                DropDownResource.Items.Add(new ListItem (title ,resourceid));
            }
            reader.Close();
            con.Close();
        }
    }


    /*
     * void ButtonRequest_Click(object sender, EventArgs e)
     * Event handler that gets invoked when the Request UI Button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method creates a new request for a resource
     */
    protected void ButtonRequest_Click(object sender, EventArgs e)
    {
        String type = TextRequest.Text;
        int resourceid = 1;
        string description = TextDescription.Text ;
        int yr = Int16.Parse(DropDownYear.SelectedItem.Text );
        int month = Int16.Parse(DropDownMonth.SelectedItem.Text );
        int day = Int16.Parse(DropDownDay.SelectedItem.Text );
        DateTime bookingdate = new DateTime(yr, month, day);
        if (DropDownResource.SelectedItem != null)
        {
            resourceid = Int16.Parse(DropDownResource.SelectedItem.Value);
            
        }
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();
            //Gettign the username from context
            string username = HttpContext.Current.User.Identity.Name;


            //Below few lines are to retrieve family ID from Username
            int familyId = 0;
            com = new SqlCommand("SELECT FAMILY_ID FROM USERS WHERE USER_NAME=@username", con);
            com.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            reader = com.ExecuteReader();
            if (reader.Read())
            {
                String familyIdString = reader["FAMILY_ID"].ToString();

                if (!string.IsNullOrEmpty(familyIdString))
                {
                    familyId = Int16.Parse(familyIdString);
                }
            }
            reader.Close();

            //Following lines actually creates the REQUEST table entry
            //Creates the SQL query
            com = new SqlCommand("INSERT INTO REQUEST (PARISH_RESOURCE_ID,FAMILY_ID,BOOKING_DATE,REQUEST_DESCRIPTION,STATUS) VALUES(@resourceid,@familyid,@bookingdate,@description,@status)", con);
            //Assigning values to bind parameters
            com.Parameters.Add("@resourceid", SqlDbType.Int).Value = resourceid;
            com.Parameters.Add("@familyid", SqlDbType.Int).Value = familyId;
            com.Parameters.Add("@bookingdate", SqlDbType.VarChar).Value =bookingdate;
            com.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
            com.Parameters.Add("@status", SqlDbType.VarChar).Value = "NOT_APPROVED";
            //Executing the query
            int result = com.ExecuteNonQuery();
            if (result == 1)
            {
                LabelMessage.Text = "Successfully created";
                TextDescription.Text = "";
            }
            else
            {
                LabelMessage.Text = "Failed to create Request";
            }
            con.Close();
        }
    }


    /*
     * void ButtonAvailability_Click(object sender, EventArgs e)
     * Event handler that gets invoked when the 'Check Availability' button is clicked
     * @params: object sender - is the UI component object which sends the event
     * @params: EventArgs e - is the set of arguments passed along with the Event
     * @return: Returns nothing
     * This method checks whether a resource is available on the selected date
     */
    protected void ButtonAvailability_Click(object sender, EventArgs e)
    {
        String type = TextRequest.Text;
        int resourceid = 1;
        string description = TextDescription.Text;
        int yr = Int16.Parse(DropDownYear.SelectedItem.Text);
        int month = Int16.Parse(DropDownMonth.SelectedItem.Text);
        int day = Int16.Parse(DropDownDay.SelectedItem.Text);
        DateTime bookingdate = new DateTime(yr, month, day);
        if (DropDownResource.SelectedItem != null)
        {
            resourceid = Int16.Parse(DropDownResource.SelectedItem.Value);

        }
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            con.Open();

            com = new SqlCommand("SELECT REQUEST_ID FROM REQUEST WHERE BOOKING_DATE=@bookingdate and PARISH_RESOURCE_ID=@resourceid", con);
            com.Parameters.Add("@resourceid", SqlDbType.Int).Value = resourceid;
            com.Parameters.Add("@bookingdate", SqlDbType.VarChar).Value = bookingdate;
            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                LabelMessage.Text = "Not available";
            }
            else
            {
                LabelMessage.Text = "Available";
            }
            reader.Close();
        }
    }
}

