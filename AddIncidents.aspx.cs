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


public partial class AddIncidents : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["basilicaConnectionString"].ToString();
    SqlCommand com;
    SqlDataReader reader;

    int incidentid = 1;
    protected override void OnInit(EventArgs e)
    {
        //Invokes the base class method
        base.OnInit(e);
        populateDateControls(DropDownYear, DropDownMonth, DropDownDay);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
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
    protected void DropDownYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownYear, DropDownMonth, DropDownDay);
    }
    protected void DropDownMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        populeDays(DropDownYear, DropDownMonth, DropDownDay);
    }

    protected void ButtonAddPhotos_Click(object sender, EventArgs e)
    {

    }

    protected void ButtonAddImage_Click(object sender, EventArgs e)
    {

    }
    protected void ButtonCreateNewIncident_Click(object sender, EventArgs e)
    {

    }
    protected void ButtonAddIncident_Click(object sender, EventArgs e)
    {
        //Code to add incident
        String incidentType = TextIncidentType.Text;
        int yr = Int16.Parse(DropDownYear.SelectedItem.Text);
        int month = Int16.Parse(DropDownMonth.SelectedItem.Text);
        int day = Int16.Parse(DropDownDay.SelectedItem.Text);
        DateTime incidentdate = new DateTime(yr, month, day);
        String description = TextDescription.Text;
        byte[] imagedata=FileUploadImage.FileBytes ;
        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString))
        {
            string username = HttpContext.Current.User.Identity.Name;

            int familyId = 0;
            con.Open();
            com = new SqlCommand("SELECT FAMILY_ID FROM USERS WHERE USER_NAME=@username", con);
            com.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                String familyIdString = reader["FAMILY_ID"].ToString();

                if (!string.IsNullOrEmpty(familyIdString))
                {
                    familyId = Int16.Parse(familyIdString);
                }
            }
            reader.Close();

            com = new SqlCommand("INSERT INTO INCIDENTS(INCIDENT_TYPE,INCIDENT_DATE,FAMILY_ID,INCIDENT_DESCRIPTION,IMAGE_DATA)VALUES(@incidenttype,@incidentdate,@familyid,@description,@imagedata)", con);
            com.Parameters.Add("@incidenttype", SqlDbType.VarChar).Value = incidentType ;
            com.Parameters.Add("@incidentdate", SqlDbType.DateTime).Value = incidentdate ;
            com.Parameters.Add("@familyid", SqlDbType.Int).Value = familyId;
            com.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
            com.Parameters.Add("@imagedata", SqlDbType.VarBinary).Value =imagedata ;

        
            com.ExecuteNonQuery();
            LabelMessage.Text = "created new incident";
        }
    }

    protected void ImageButtonDeath_Click(object sender, ImageClickEventArgs e)
    {
        TextIncidentType.Text = "Death";
        
    }
    protected void ImageButtonWedding_Click(object sender, ImageClickEventArgs e)
    {
        TextIncidentType.Text = "Wedding";
    }
    protected void ImageButtonBirth_Click(object sender, ImageClickEventArgs e)
    {
        TextIncidentType.Text = "Birth";
    }
}
