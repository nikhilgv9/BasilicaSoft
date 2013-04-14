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

public partial class ViewIncident : System.Web.UI.Page
{

    /*Class memebers*/

    SqlCommand com;
    SqlDataReader reader;
    static int rowid = 1;
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
        PopulateCurrentIncidentDetails();
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
        PopulateCurrentIncidentDetails();
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
        PopulateCurrentIncidentDetails();
    }
    /*
     * void PopulateCurrentEventDetails()
     * @params none
     * @return none
     * Populates all the details related to the selected event
    */

    protected void PopulateCurrentIncidentDetails()
    {
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            //Creates SQL connection
            int rowid = Int16.Parse(LabelRowId.Text);

            con.Open();
            com = new SqlCommand("SELECT * FROM INCIDENTS_VIEW WHERE ROW_ID=@rowid", con);
            com.Parameters.Add("@rowid", SqlDbType.Int).Value = rowid;
            reader = com.ExecuteReader();
            String incidentid = "";
            String incidenttype = "";
            String familyName = "";
            String incidentdate = "";
            if (reader.Read())
            {
                incidentid = reader["INCIDENT_ID"].ToString();
                incidenttype= reader["INCIDENT_TYPE"].ToString();
                familyName = reader["FAMILY_NAME"].ToString();
                incidentdate = ((DateTime)reader["INCIDENT_DATE"]).ToShortDateString();
            }
            reader.Close();
            LabelName.Text = incidenttype;
            LabelDate.Text = incidentdate;
            LabelFamily.Text = familyName;
            Image1.ImageUrl = "GetImage.aspx?incident=true&imageId=" + incidentid;
        }
    }
}
